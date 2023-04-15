﻿using IntegrationModule.Models;
using IntegrationModule.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
