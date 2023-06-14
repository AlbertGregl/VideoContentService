using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {

        private readonly RwaMoviesContext _dbContext;

        public CountriesController(RwaMoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Countries
        [HttpGet]
        public ActionResult<IEnumerable<CountryResponse>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                // Get countries from database
                var countries = _dbContext.Countries
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                // Map to response model
                var response = countries.Select(c => new CountryResponse
                {
                    Code = c.Code,
                    Name = c.Name,
                });
                // Return response
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
