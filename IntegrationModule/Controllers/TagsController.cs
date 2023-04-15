using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly RwaMoviesContext _dbContext;
        public TagsController(RwaMoviesContext dbContext)
        {
            _dbContext = dbContext;

        }

        // GET method to retrieve all tags with associated video tags
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<TagResponse>> GetAll()
        {
            var tags = _dbContext.Tags;
            var tagResponses = new List<TagResponse>();
            foreach (var tag in tags)
            {
                var tagResponse = new TagResponse
                {
                    Id = tag.Id,
                    Name = tag.Name
                };
                foreach (var videoTag in tag.VideoTags)
                {
                    var videoTagResponse = new VideoTagResponse
                    {
                        Id = videoTag.Id,
                        VideoId = videoTag.VideoId,
                        TagId = videoTag.TagId
                    };
                    tagResponse.VideoTags.Add(videoTagResponse);
                }
                tagResponses.Add(tagResponse);
            }
            return tagResponses;
        }

        // GET method to retrieve tag by ID with associated video tags
        [HttpGet("[action]/{id}")]
        public ActionResult<TagResponse> GetById(int id)
        {
            var tag = _dbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            var tagResponse = new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name
            };
            foreach (var videoTag in tag.VideoTags)
            {
                var videoTagResponse = new VideoTagResponse
                {
                    Id = videoTag.Id,
                    VideoId = videoTag.VideoId,
                    TagId = videoTag.TagId
                };
                tagResponse.VideoTags.Add(videoTagResponse);
            }
            return tagResponse;
        }

        // POST method to create a new tag
        [HttpPost("[action]")]
        public ActionResult<TagResponse> CreateTag([FromBody] TagRequest tagRequest)
        {
            var tag = new Tag
            {
                Name = tagRequest.Name
            };
            _dbContext.Tags.Add(tag);
            _dbContext.SaveChanges();
            var tagResponse = new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name
            };
            return tagResponse;
        }

        // POST method to create a new video tag
        [HttpPost("[action]")]
        public ActionResult<VideoTagResponse> CreateVideoTag([FromBody] VideoTagRequest videoTagRequest)
        {            
            var videoTag = new VideoTag
            {
                VideoId = videoTagRequest.VideoId,
                TagId = videoTagRequest.TagId
            };
            _dbContext.VideoTags.Add(videoTag);
            _dbContext.SaveChanges();
            var videoTagResponse = new VideoTagResponse
            {
                Id = videoTag.Id,
                VideoId = videoTag.VideoId,
                TagId = videoTag.TagId
            };
            return videoTagResponse;
        }

        // PUT method to update a tag
        [HttpPut("[action]/{id}")]
        public ActionResult<TagResponse> UpdateTag(int id, [FromBody] TagRequest tagRequest)
        {
            var tag = _dbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            tag.Name = tagRequest.Name;
            _dbContext.SaveChanges();
            var tagResponse = new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name
            };
            return tagResponse;
        }

        // PUT method to update a video tag
        [HttpPut("[action]/{id}")]
        public ActionResult<VideoTagResponse> UpdateVideoTag(int id, [FromBody] VideoTagRequest videoTagRequest)
        {


            var videoTag = _dbContext.VideoTags.Find(id);
            if (videoTag == null)
            {
                return NotFound();
            }
            videoTag.VideoId = videoTagRequest.VideoId;
            videoTag.TagId = videoTagRequest.TagId;
            _dbContext.SaveChanges();
            var videoTagResponse = new VideoTagResponse
            {
                Id = videoTag.Id,
                VideoId = videoTag.VideoId,
                TagId = videoTag.TagId
            };
            return videoTagResponse;
        }

        // DELETE method to delete a tag
        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteTag(int id)
        {
            var tag = _dbContext.Tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }
            _dbContext.Tags.Remove(tag);
            _dbContext.SaveChanges();
            return Ok();
        }

        // DELETE method to delete a video tag
        [HttpDelete("[action]/{id}")]
        public ActionResult DeleteVideoTag(int id)
        {
            var videoTag = _dbContext.VideoTags.Find(id);
            if (videoTag == null)
            {
                return NotFound();
            }
            _dbContext.VideoTags.Remove(videoTag);
            _dbContext.SaveChanges();
            return Ok();
        }


    }
}
 