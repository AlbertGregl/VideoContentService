using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;
using VideoContentService.Public.Services;

namespace VideoContentService.Public.Controllers
{
    public class PublicUserController : Controller
    {
        private readonly PublicUserService _publicUserService;

        public PublicUserController(PublicUserService publicService)
        {
            _publicUserService = publicService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _publicUserService.RegisterUserAsync(user);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "User created successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Failed to create user. Please try again.";
                        return View(user);
                    }
                }
                else
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while creating the user.";
                // Log the exception message: ex.Message
                return RedirectToAction("Error");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _publicUserService.LoginUserAsync(userLoginRequest);

                    if (response != null)
                    {
                        var tokens = await _publicUserService.GetJwtTokensAsync(new JwtTokensRequest
                        {
                            Username = userLoginRequest.Username,
                            Password = userLoginRequest.Password
                        });

                        if (tokens != null)
                        {
                            // Store the token, username and id in a dictionary
                            var userData = new Dictionary<string, string>
                            {
                                { "Token", tokens.Token },
                                { "Username", userLoginRequest.Username },
                                { "Id", response.Id.ToString() }
                            };
                            // Pass the dictionary to the view
                            return View("LoginSuccess", userData);
                        }
                        else
                        {
                            TempData["Error"] = "Invalid login credentials.";
                            return View(userLoginRequest);
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Invalid login credentials.";
                        return View(userLoginRequest);
                    }
                }
                else
                {
                    return View(userLoginRequest);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred during login.";
                return RedirectToAction("Error");
            }
        }



        public IActionResult Error()
        {
            return View();
        }
    }
}

