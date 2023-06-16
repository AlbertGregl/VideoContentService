using IntegrationModule.Models;
using IntegrationModule.BLModels;
using IntegrationModule.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly RwaMoviesContext _dbContext;

        public UsersController(RwaMoviesContext dbContext, IUserRepository userRepo)
        {
            _dbContext = dbContext;
            _userRepository = userRepo;
        }


        [HttpPost("[action]")]
        public ActionResult ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Normalize and check if username exists in database
            var username = request.Username.ToLower();
            var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);
            if (user == null)
            {
                return BadRequest(new ChangePasswordResponse { IsSuccess = false, Message = "User doesn't exist" });
            }

            // Check if old password is correct
            if (!Authenticate(username, request.CurrentPassword))
            {
                return BadRequest(new ChangePasswordResponse { IsSuccess = false, Message = "Old password is incorrect" });
            }

            // Check if the new password is the same as the old password
            if (Authenticate(username, request.NewPassword))
            {
                return BadRequest(new ChangePasswordResponse { IsSuccess = false, Message = "New password is the same as old password" });
            }

            // Generate a new salt
            byte[] newSalt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(newSalt);
            }

            // Hash the new password with the same settings as in Authenticate method
            var newPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: request.NewPassword,
                salt: newSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Set new password and salt
            user.PwdHash = newPassword;
            user.PwdSalt = Convert.ToBase64String(newSalt);

            // Save changes to the database
            _dbContext.SaveChanges();

            return Ok(new ChangePasswordResponse { IsSuccess = true, Message = "Password changed successfully" });
        }


        // action for register user

        [HttpPost("[action]")]
        public ActionResult<User> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Normalize and check if username exists in database
                var username = request.Username.ToLower();
                if (_dbContext.Users.Any(x => x.Username == username))
                {
                    throw new InvalidOperationException("Username already exists");
                }
                // check if request.CountryOfResidenceId exists in database
                if (!_dbContext.Countries.Any(x => x.Id == request.CountryOfResidenceId))
                {
                    throw new InvalidOperationException("Country of residence doesn't exist");
                }

                // set user data
                var newUser = _userRepository.Add(request);
                // save user to the database
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                // notification body
                var body = $"Please confirm your email by clicking on the following link: https://localhost:7078/validate-email.html?username={newUser.Username}&b64SecToken={newUser.SecurityToken}";
     
                // create notification
                var notification = new Notification
                {
                    CreatedAt = DateTime.UtcNow,
                    ReceiverEmail = newUser.Email,
                    Subject = "Email confirmation",
                    Body = body
                };
                // save notification to the database
                _dbContext.Notifications.Add(notification);
                _dbContext.SaveChanges();

                return Ok(new UserRegisterResponse
                {
                    Id = newUser.Id,
                    SecurityToken = newUser.SecurityToken
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("[action]")]
        public ActionResult<UserLoginResponse> Login([FromBody] UserLoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Normalize and check if username exists in the database
                var username = request.Username.ToLower();
                var user = _dbContext.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                {
                    return Unauthorized("Invalid username.");
                }

                // Authenticate the user
                bool isAuthenticated = Authenticate(request.Username, request.Password);

                if (!isAuthenticated)
                    return Unauthorized("Invalid password.");

                // Generate token from JwtTokens
                var tokens = _userRepository.JwtTokens(new JwtTokensRequest
                {
                    Username = request.Username,
                    Password = request.Password
                });

                // Return user details along with the token
                return Ok(new UserLoginResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Token = tokens.Token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("[action]")]
        public ActionResult ValidateEmail([FromBody] ValidateEmailRequest request)
        {
            try
            {
                var target = _dbContext.Users.FirstOrDefault(x =>
                    x.Username == request.Username && x.SecurityToken == request.B64SecToken);

                if (target == null)
                {
                    throw new InvalidOperationException("Authentication failed");
                }

                target.IsConfirmed = true;

                // save user validation field
                _dbContext.Users.Update(target);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult<Tokens> JwtTokens([FromBody] JwtTokensRequest request)
        {
            try
            {
                var isAuthenticated = Authenticate(request.Username, request.Password);

                if (!isAuthenticated)
                    throw new InvalidOperationException("Authentication failed");

                return Ok(_userRepository.JwtTokens(request));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private bool Authenticate(string username, string password)
        {
            var target = _dbContext.Users.FirstOrDefault(x => x.Username == username);

            if (!target.IsConfirmed)
                return false;

            // Get stored salt and hash
            byte[] salt = Convert.FromBase64String(target.PwdSalt);
            byte[] hash = Convert.FromBase64String(target.PwdHash);

            byte[] calcHash =
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8);

            return hash.SequenceEqual(calcHash);
        }

    }
}
