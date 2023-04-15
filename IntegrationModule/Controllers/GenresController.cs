using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly RwaMoviesContext _dbContext;
        public GenresController(RwaMoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET method to retrieve all genres
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<GenreResponse>> GetAll()
        {
            try
            {
                var allGenres = _dbContext.Genres.Select(dbGenre =>
                    new GenreResponse
                    {
                        Id = dbGenre.Id,
                        Name = dbGenre.Name,
                        Description = dbGenre.Description
                    });
                return Ok(allGenres);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET method to retrieve a specific genre by ID
        [HttpGet("[action]/{id}")]
        public ActionResult<GenreResponse> GetById(int id)
        {
            try
            {
                var dbGenre = _dbContext.Genres.Find(id);
                if (dbGenre == null)
                {
                    return NotFound();
                }
                var genre = new GenreResponse
                {
                    Id = dbGenre.Id,
                    Name = dbGenre.Name,
                    Description = dbGenre.Description
                };
                return Ok(genre);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST method to create new genre
        [HttpPost("[action]")]
        public ActionResult<GenreResponse> Create([FromBody] GenreRequest genreRequest)
        {
            try
            {
                var dbGenre = new Genre
                {
                    Name = genreRequest.Name,
                    Description = genreRequest.Description,
                };
                _dbContext.Genres.Add(dbGenre);
                _dbContext.SaveChanges();
                var genre = new GenreResponse
                {
                    Id = dbGenre.Id,
                    Name = dbGenre.Name,
                    Description = dbGenre.Description
                };
                return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT method to update existing genre by ID
        [HttpPut("[action]/{id}")]
        public ActionResult<GenreResponse> Update(int id, [FromBody] GenreRequest genreRequest)
        {
            try
            {
                var dbGenre = _dbContext.Genres.Find(id);
                if (dbGenre == null)
                {
                    return NotFound();
                }
                dbGenre.Name = genreRequest.Name;
                dbGenre.Description = genreRequest.Description;
                _dbContext.SaveChanges();
                var genre = new GenreResponse
                {
                    Id = dbGenre.Id,
                    Name = dbGenre.Name,
                    Description = dbGenre.Description
                };
                return Ok(genre);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE method to delete existing genre by ID
        [HttpDelete("[action]/{id}")]
        public ActionResult<GenreResponse> Delete(int id)
        {
            try
            {
                var dbGenre = _dbContext.Genres.Find(id);
                if (dbGenre == null)
                {
                    return NotFound();
                }
                _dbContext.Genres.Remove(dbGenre);
                _dbContext.SaveChanges();
                var genre = new GenreResponse
                {
                    Id = dbGenre.Id,
                    Name = dbGenre.Name
                };
                return Ok(genre);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
 