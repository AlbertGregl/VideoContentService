using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUsersController : ControllerBase
    {
        private readonly RwaMoviesContext _dbContext;

        public ManageUsersController(RwaMoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET method to retrieve all users with optional filters
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<UserResponse>> GetAll(string firstNameFilter = null, string lastNameFilter = null, string usernameFilter = null, string countryFilter = null)
        {
            try
            {
                var query = _dbContext.Users.AsQueryable();

                if (!string.IsNullOrEmpty(firstNameFilter))
                {
                    query = query.Where(user => user.FirstName.Contains(firstNameFilter));
                }

                if (!string.IsNullOrEmpty(lastNameFilter))
                {
                    query = query.Where(user => user.LastName.Contains(lastNameFilter));
                }

                if (!string.IsNullOrEmpty(usernameFilter))
                {
                    query = query.Where(user => user.Username.Contains(usernameFilter));
                }

                if (!string.IsNullOrEmpty(countryFilter))
                {
                    query = query.Where(user => user.CountryOfResidence.Name.Contains(countryFilter));
                }

                var users = query.Select(dbUser => new UserResponse
                {
                    Id = dbUser.Id,
                    Username = dbUser.Username,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Email = dbUser.Email,
                    CountryOfResidenceId = dbUser.CountryOfResidenceId,
                    Phone = dbUser.Phone,
                    IsConfirmed = dbUser.IsConfirmed,
                    DeletedAt = dbUser.DeletedAt
                }).ToList();

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET method to retrieve a specific user by ID
        [HttpGet("[action]/{id}")]
        public ActionResult<UserResponse> GetById(int id)
        {
            try
            {
                var dbUser = _dbContext.Users.Find(id);
                if (dbUser == null)
                {
                    return NotFound();
                }

                var user = new UserResponse
                {
                    Id = dbUser.Id,
                    Username = dbUser.Username,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Email = dbUser.Email,
                    CountryOfResidenceId = dbUser.CountryOfResidenceId,
                    Phone = dbUser.Phone,
                    IsConfirmed = dbUser.IsConfirmed,
                    DeletedAt = dbUser.DeletedAt
                };

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT method to update existing user by ID
        [HttpPut("[action]/{id}")]
        public ActionResult<UserResponse> Update(int id, [FromBody] UserRequest userRequest)
        {
            try
            {
                var dbUser = _dbContext.Users.Find(id);
                if (dbUser == null)
                {
                    return NotFound();
                }

                // Update user properties
                dbUser.Username = userRequest.Username;
                dbUser.FirstName = userRequest.FirstName;
                dbUser.LastName = userRequest.LastName;
                dbUser.Email = userRequest.Email;
                dbUser.Phone = userRequest.Phone;
                dbUser.IsConfirmed = userRequest.IsConfirmed;
                dbUser.CountryOfResidenceId = userRequest.CountryOfResidenceId;

                _dbContext.SaveChanges();

                var user = new UserResponse
                {
                    Id = dbUser.Id,
                    Username = dbUser.Username,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Email = dbUser.Email,
                    CountryOfResidenceId = dbUser.CountryOfResidenceId,
                    Phone = dbUser.Phone,
                    IsConfirmed = dbUser.IsConfirmed,
                    DeletedAt = dbUser.DeletedAt
                };

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE method to soft delete a user by ID
        [HttpDelete("[action]/{id}")]
        public ActionResult<UserResponse> SoftDelete(int id)
        {
            try
            {
                var dbUser = _dbContext.Users.Find(id);
                if (dbUser == null)
                {
                    return NotFound();
                }

                // Mark user as soft deleted by setting DeletedAt to current date and time
                dbUser.DeletedAt = DateTime.UtcNow;

                _dbContext.SaveChanges();

                var user = new UserResponse
                {
                    Id = dbUser.Id,
                    Username = dbUser.Username,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Email = dbUser.Email,
                    CountryOfResidenceId = dbUser.CountryOfResidenceId,
                    Phone = dbUser.Phone,
                    IsConfirmed = dbUser.IsConfirmed,
                    DeletedAt = dbUser.DeletedAt
                };

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
