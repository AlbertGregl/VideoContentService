using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;
using VideoContentService.Public.Services;

namespace VideoContentService.Public.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserProfileService _userProfileService;

        public UserProfileController(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public IActionResult Profile()
        {
            return View();
        }

        // Action Method for AJAX call to get user details by id
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userProfileService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Json(user);
        }

        // Action Method to render the ChangePassword view
        public IActionResult ChangePassword()
        {
            return View();
        }

        // Action method to handle the change password request
        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            HttpResponseMessage response = await _userProfileService.ChangePasswordAsync(changePasswordRequest);
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error changing password");
            }
        }
    }
}