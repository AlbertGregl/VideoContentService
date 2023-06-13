using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoContentService.Admin.Services;

namespace VideoContentService.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminApiService _adminApiService;
        private readonly IMapper _mapper;

        // Inject the admin API service and automapper
        public AdminController(IAdminApiService adminApiService, IMapper mapper)
        {
            _adminApiService = adminApiService;
            _mapper = mapper;
        }

        // Action method for managing videos
        public IActionResult ManageVideos()
        {
            // Call the service to get the list of videos
            // var videos = _adminApiService.GetVideos();
            // return View(videos);
            return View();
        }

        // Action method for managing countries
        public IActionResult ManageCountries()
        {
            // Call the service to get the list of countries
            // var countries = _adminApiService.GetCountries();
            // return View(countries);
            return View();
        }

        // Action method for managing tags
        public IActionResult ManageTags()
        {
            // Call the service to get the list of tags
            // var tags = _adminApiService.GetTags();
            // return View(tags);
            return View();
        }

        // Action method for managing genres
        public IActionResult ManageGenres()
        {
            // Call the service to get the list of genres
            // var genres = _adminApiService.GetGenres();
            // return View(genres);
            return View();
        }
    }
}
