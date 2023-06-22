using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly RwaMoviesContext _dbContext;

        public ImagesController(RwaMoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetImageUrl(int id)
        {
            var image = _dbContext.Images.Find(id);

            if (image == null)
            {
                return NotFound();
            }

            // image.Content is a string representing the image URL
            return Ok(image.Content);
        }

        // get all images use try catch
        [HttpGet]
        public ActionResult<IEnumerable<Image>> GetAllImages()
        {
            try
            {
                var images = _dbContext.Images.ToList();
                return Ok(images);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
