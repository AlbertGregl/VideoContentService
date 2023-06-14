using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    // Specifies the base route for all actions in this controller
    [Route("api/[controller]")]
    // Specifies that this controller should automatically respond with an HTTP 400 BadRequest 
    // if the request's body cannot be deserialized to the expected input model.
    [ApiController]
    public class VideosController : ControllerBase
    {
        // The database context used by this controller.
        private readonly RwaMoviesContext _dbContext;

        // Constructor that initializes the database context via dependency injection.
        public VideosController(RwaMoviesContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET method to retrieve all videos with support for paging, filtering and ordering, USER MUST BE AUTHORIZED
        [Authorize]
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<VideoResponse>> GetAll([FromQuery] string name, [FromQuery] string genre, [FromQuery] string orderBy, [FromQuery] int? page, [FromQuery] int? pageSize)
        {
            try
            {
                // Retrieve all video entities from the database
                var videos = _dbContext.Videos.AsQueryable();
                // Apply filtering by name or partial name
                if (!string.IsNullOrWhiteSpace(name))
                {
                    videos = videos.Where(v => v.Name.ToLower().Contains(name.ToLower()));
                }
                // Apply filtering by genre 
                if (!string.IsNullOrWhiteSpace(genre))
                {
                    videos = videos.Where(v => v.Genre.Name.ToLower().Contains(genre.ToLower()));
                }


                // Apply ordering id, name and total time
                if (!string.IsNullOrWhiteSpace(orderBy))
                {
                    switch (orderBy.ToLower())
                    {
                        case "id":
                            videos = videos.OrderBy(v => v.Id);
                            break;
                        case "name":
                            videos = videos.OrderBy(v => v.Name);
                            break;
                        case "totaltime":
                            videos = videos.OrderBy(v => v.TotalSeconds);
                            break;
                        default:
                            videos = videos.OrderBy(v => v.Id);
                            break;
                    }
                }
                else
                {
                    videos = videos.OrderBy(v => v.Id);
                }
                // Apply paging
                if (page.HasValue && pageSize.HasValue)
                {
                    videos = videos.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }               

                // Map the video entities to their corresponding DTOs
                var videoResponses = videos.Select(v => new VideoResponse
                {
                    Id = v.Id,
                    Name = v.Name,
                    Description = v.Description,
                    GenreId = v.GenreId,
                    TotalSeconds = v.TotalSeconds,
                    StreamingUrl = v.StreamingUrl,
                    ImageId = v.ImageId,
                    Tags = v.VideoTags.Select(vt => new TagResponse
                    {
                        Id = vt.Tag.Id,
                        Name = vt.Tag.Name
                    }).ToList()
                });
                // Return the mapped DTOs
                return Ok(videoResponses);
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error response to the client
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET method to retrieve a specific video by ID
        [Authorize]
        [HttpGet("[action]/{id}")]
        public ActionResult<VideoResponse> GetById(int id)
        {
            try
            {
                // Retrieve the video entity with the specified ID from the database
                var video = _dbContext.Videos.Find(id);
                // If the video entity was not found, return a 404 Not Found response to the client
                if (video == null)
                {
                    return NotFound();
                }
                // Map the video entity to its corresponding DTO
                var videoResponse = new VideoResponse
                {
                    Id = video.Id,
                    Name = video.Name,
                    Description = video.Description,
                    GenreId = video.GenreId,
                    TotalSeconds = video.TotalSeconds,
                    StreamingUrl = video.StreamingUrl,
                    ImageId = video.ImageId,
                    Tags = video.VideoTags.Select(vt => new TagResponse
                    {
                        Id = vt.Tag.Id,
                        Name = vt.Tag.Name
                    }).ToList()
                };
                // Return the mapped DTO
                return Ok(videoResponse);
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error response to the client
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST method to create a new video
        [Authorize]
        [HttpPost("[action]")]
        public ActionResult<VideoResponse> Create([FromBody] VideoRequest videoRequest)
        {
            try
            {
                Genre? genre;
                Image? image;
                CreateOrUpdateGenreAndImage(videoRequest, out genre, out image);

                // Map the video request DTO to a new video entity
                var video = new Video
                {
                    Name = videoRequest.Name,
                    Description = videoRequest.Description,
                    GenreId = genre.Id,
                    TotalSeconds = videoRequest.TotalSeconds,
                    StreamingUrl = videoRequest.StreamingUrl,
                    ImageId = image.Id
                };
                // Add the new video entity to the database
                _dbContext.Videos.Add(video);
                // Save the changes to the database
                _dbContext.SaveChanges();
                // Map the newly created video entity to its corresponding DTO
                var videoResponse = new VideoResponse
                {
                    Id = video.Id,
                    Name = video.Name,
                    Description = video.Description,
                    GenreId = video.GenreId,
                    TotalSeconds = video.TotalSeconds,
                    StreamingUrl = video.StreamingUrl,
                    ImageId = video.ImageId
                };
                // Return the mapped DTO
                return Ok(videoResponse);
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error response to the client
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT method to update an existing video
        [Authorize]
        [HttpPut("[action]/{id}")]
        public ActionResult<VideoResponse> Update(int id, [FromBody] VideoRequest videoRequest)
        {
            try
            {
                Genre? genre;
                Image? image;
                CreateOrUpdateGenreAndImage(videoRequest, out genre, out image);

                // Retrieve the video entity with the specified ID from the database
                var video = _dbContext.Videos.Find(id);
                // If the video entity was not found, return a 404 Not Found response to the client
                if (video == null)
                {
                    return NotFound();
                }
                // Update the video entity with the values from the video request DTO
                video.Name = videoRequest.Name;
                video.Description = videoRequest.Description;
                video.GenreId = genre.Id;
                video.TotalSeconds = videoRequest.TotalSeconds;
                video.StreamingUrl = videoRequest.StreamingUrl;
                video.ImageId = image.Id;
                // Save the changes to the database
                _dbContext.SaveChanges();
                // Map the updated video entity to its corresponding DTO
                var videoResponse = new VideoResponse
                {
                    Id = video.Id,
                    Name = video.Name,
                    Description = video.Description,
                    GenreId = video.GenreId,
                    TotalSeconds = video.TotalSeconds,
                    StreamingUrl = video.StreamingUrl,
                    ImageId = video.ImageId
                };
                // Return the mapped DTO
                return Ok(videoResponse);
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error response to the client
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE method to delete an existing video
        [Authorize]
        [HttpDelete("[action]/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // Retrieve the video entity with the specified ID from the database
                var video = _dbContext.Videos.Find(id);
                // If the video entity was not found, return a 404 Not Found response to the client
                if (video == null)
                {
                    return NotFound();
                }

                // Remove image entity from the database where the image is not used by any other video
                var image = _dbContext.Images.Find(video.ImageId);
                if (image != null && _dbContext.Videos.Count(v => v.ImageId == image.Id) == 1)
                {
                    _dbContext.Images.Remove(image);
                }

                // Remove all VideoTags from database where the video exists
                var videoTags = _dbContext.VideoTags.Where(vt => vt.VideoId == video.Id);
                if (videoTags.Any())
                {
                    _dbContext.VideoTags.RemoveRange(videoTags);
                }

                // Remove the video entity from the database
                _dbContext.Videos.Remove(video);
                // Save the changes to the database
                _dbContext.SaveChanges();
                // Return a 204 No Content response to the client
                return NoContent();
            }
            catch (Exception)
            {
                // Return a 500 Internal Server Error response to the client
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
        private void CreateOrUpdateGenreAndImage(VideoRequest videoRequest, out Genre? genre, out Image? image)
        {
            // Normalize the genre name by converting it to lowercase
            var genreName = videoRequest.GenreName.ToLower();
            // Check if the specified genre exists in the database
            genre = _dbContext.Genres.FirstOrDefault(g => g.Name == videoRequest.GenreName);
            // If the genre doesn't exist, create a new genre entity
            if (genre == null)
            {
                genre = new Genre
                {
                    Name = videoRequest.GenreName
                };
                _dbContext.Genres.Add(genre);
            }

            // Check if the specified image exists in the database
            image = _dbContext.Images.FirstOrDefault(i => i.Content == videoRequest.ImageUrl);
            // Create a new image entity
            if (image == null)
            {
                image = new Image
                {
                    Content = videoRequest.ImageUrl
                };
                // Add the new image entity to the database
                _dbContext.Images.Add(image);
            }
            // Save the changes to the database
            _dbContext.SaveChanges();
        }

    }
}
 