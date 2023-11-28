﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using IS220_WebApplication.Areas.Admin.Models.Authentication;
using IS220_WebApplication.Context;
using IS220_WebApplication.Database;
using IS220_WebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static IS220_WebApplication.Database.UsersProcessor;

namespace IS220_WebApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    public class LoginController : Controller
    {
        private readonly INotyfService? _notyf;
        private readonly UserManager<Aspnetuser> _userManager;
        private readonly SignInManager<Aspnetuser> _signInManager;


        public LoginController(INotyfService notyf, UserManager<Aspnetuser> userManager, SignInManager<Aspnetuser> signInManager)
        {
            _notyf = notyf;
            _userManager = userManager;
            _signInManager = signInManager; 
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity is { IsAuthenticated: true }) return RedirectToAction("index", "dashboard");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Aspnetuser user)
        {
            ModelState.Remove("Status");
            if (!ModelState.IsValid) return View();

            // Check if the user exists
            var existingUser = await _userManager.FindByNameAsync(user.UserName!);
            if (existingUser == null)
            {
                _notyf?.Error("User not exist");
                return View();
            }

            if (existingUser.Role != 1)
            {
                _notyf?.Error("You don't have permission to access this page");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName!, user.PasswordHash!, false, false);

            if (result.Succeeded)
            {
                var originalUrl = HttpContext.Request.Cookies["OriginalUrl"];
                HttpContext.Response.Cookies.Delete("OriginalUrl");
                
                if (!string.IsNullOrEmpty(originalUrl))
                {
                    return LocalRedirect(originalUrl);
                }
                _notyf?.Success("Login successful");
                return RedirectToAction("index", "dashboard");
            }
            
            _notyf?.Error("Login failed");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _notyf?.Success("You have been logged out");
            return RedirectToAction("index", "login", new { area = "admin" });
        }

        public void CheckModelState()
        {
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal?.Errors!)
                {
                    // Log or print the error message
                    Console.WriteLine($"{modelStateKey}: {error.ErrorMessage}");
                }
            }
        }
    }
}