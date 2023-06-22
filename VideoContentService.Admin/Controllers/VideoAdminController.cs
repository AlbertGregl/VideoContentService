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

        public async Task<IActionResult> Index(string filter, int page = 1)
        {
            try
            {
                // JWT token for the administrative user
                await _videoService.ObtainJwtTokenForAdmin(_username, _password);

                // Get paginated videos
                var videos = await _videoService.GetAllVideosAsync(page, filter);

                // Pass the current filter to the view
                ViewData["CurrentFilter"] = filter;

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
            try
            {
                await _videoService.ObtainJwtTokenForAdmin(_username, _password);
                await _videoService.CreateVideoAsync(video);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                await _videoService.ObtainJwtTokenForAdmin(_username, _password);
                var video = await _videoService.GetVideoByIdAsync(id);
                return View(video);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VideoRequest video)
        {
            try
            {
                await _videoService.ObtainJwtTokenForAdmin(_username, _password);
                await _videoService.UpdateVideoAsync(id, video);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _videoService.ObtainJwtTokenForAdmin(_username, _password);
                await _videoService.DeleteVideoAsync(id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        // This method is called from the Index view to load the next page of videos via AJAX
        [HttpGet]
        public async Task<IActionResult> LoadData(int page, int pageSize, string nameFilter, string genreFilter)
        {
            try
            {
                await _videoService.ObtainJwtTokenForAdmin(_username, _password);
                var videos = await _videoService.GetAllVideosWithGenreFilterAsync(page, pageSize, nameFilter, genreFilter);
                return Ok(videos);
            }
            catch (HttpRequestException)
            {
                return PartialView("Partials/_Error");
            }
        }

    }
}
