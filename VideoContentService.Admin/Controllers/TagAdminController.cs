using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;
using VideoContentService.Admin.Services;

namespace VideoContentService.Admin.Controllers
{
    public class TagAdminController : Controller
    {
        private readonly TagService _tagService;

        public TagAdminController(TagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get all tags
                var tags = await _tagService.GetAllTagsAsync();

                return View(tags);
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
        public async Task<IActionResult> Create(TagRequest tag)
        {
            try
            {
                await _tagService.CreateTagAsync(tag);
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
                var tag = await _tagService.GetTagByIdAsync(id);
                return View(tag);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TagRequest tag)
        {
            try
            {
                await _tagService.UpdateTagAsync(id, tag);
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
                await _tagService.DeleteTagAsync(id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
