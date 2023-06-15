using Microsoft.AspNetCore.Mvc;
using VideoContentService.Public.Services;

namespace VideoContentService.Public.Controllers
{
    public class VideoUserController : Controller
    {
        private readonly VideoUserService _videoUserService;

        public VideoUserController(VideoUserService videoUserService)
        {
            _videoUserService = videoUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FetchVideos(string name, string genre, string orderBy, int page, int pageSize)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var videos = await _videoUserService.GetAllVideosAsync(name, genre, orderBy, page, pageSize, token);
            return Json(videos);
        }

    }
}

