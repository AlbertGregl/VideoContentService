﻿using IntegrationModule.Models;
using Microsoft.AspNetCore.Mvc;
using VideoContentService.Admin.Services;

namespace VideoContentService.Admin.Controllers
{
    public class UserAdminController : Controller
    {
        private readonly UserService _userService;

        public UserAdminController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(string firstNameFilter = null, string lastNameFilter = null, string usernameFilter = null, string countryFilter = null)
        {
            try
            {
                // Get all users with filters
                var users = await _userService.GetAllUsersAsync(firstNameFilter, lastNameFilter, usernameFilter, countryFilter);

                return View(users);
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
        public async Task<IActionResult> Create(UserRegisterRequest user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _userService.CreateUserAsync(user);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "User created successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Failed to create user. Please try again.";
                        return RedirectToAction("Create");
                    }
                }
                else
                {
                    // Return with validation errors
                    return View(user);
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while creating the user.";
                return RedirectToAction("Error", "Home");
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return View(user);
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserRequest user)
        {
            try
            {
                await _userService.UpdateUserAsync(id, user);
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
                await _userService.SoftDeleteUserAsync(id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
