using AutoMapper;
using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VideoContentService.Admin.Properties;
using VideoContentService.Admin.Services;

namespace VideoContentService.Admin.Controllers
{
    public class VideoAdminController : Controller
    {
        private readonly VideoService _videoService;
        private readonly string _username;
        private readonly string _password;

        public VideoAdminController(VideoService videoService, IOptions<AdminConfig> adminConfig)
        {
            _videoService = videoService;
            _username = adminConfig.Value.Username;
            _password = adminConfig.Value.Password;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // JWT token for the administrative user
                await _videoService.ObtainJwtTokenForAdmin(_username, _password);
                // Get all videos
                var videos = await _videoService.GetAllVideosAsync(1, "");
                return View(videos);
            }
            catch (HttpRequestException)
            {
                // Redirect to a custom error page or return an error view.
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VideoRequest video)
        {
            await _videoService.CreateVideoAsync(video);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var video = await _videoService.GetVideoByIdAsync(id);
            return View(video);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VideoRequest video)
        {
            await _videoService.UpdateVideoAsync(id, video);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _videoService.DeleteVideoAsync(id);
            return RedirectToAction("Index");
        }


    }
}
