using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;
using VideoContentService.Admin.Services;

namespace VideoContentService.Admin.Controllers
{
    public class GenreAdminController : Controller
    {
        private readonly GenreService _genreService;

        public GenreAdminController(GenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAllGenres()
        {
            try
            {
                var genres = await _genreService.GetAllGenresAsync();
                return Json(genres);
            }
            catch (HttpRequestException)
            {
                return Json(new { error = "Error fetching genres" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> CreateAjax(GenreRequest genre)
        {
            try
            {
                await _genreService.CreateGenreAsync(genre);
                return Json(new { success = true });
            }
            catch (HttpRequestException)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> EditAjax(int id, GenreRequest genre)
        {
            try
            {
                await _genreService.UpdateGenreAsync(id, genre);
                return Json(new { success = true });
            }
            catch (HttpRequestException)
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAjax(int id)
        {
            try
            {
                await _genreService.DeleteGenreAsync(id);
                return Json(new { success = true });
            }
            catch (HttpRequestException)
            {
                return Json(new { success = false });
            }
        }
    }
}
