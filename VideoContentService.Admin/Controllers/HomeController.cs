using Microsoft.AspNetCore.Mvc;

namespace VideoContentService.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
