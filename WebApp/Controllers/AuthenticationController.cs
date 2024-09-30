using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<User> _signInManager;
        public AuthenticationController(ILogger<AuthenticationController> logger, SignInManager<User> _signInManager)
        {
            this._signInManager = _signInManager;
            _logger = logger;
        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username","Password","RememberMe")]LoginDto model){
            if(ModelState.IsValid){
                var result = await _signInManager.PasswordSignInAsync(model.Username!,model.Password!,model.RememberMe,false);
                if(result.Succeeded){
                    return RedirectToAction("Index","Home");
                }
                TempData["Error_Data"]="Initento de Inicio de Sesión no valido";
                return View(model);
            }
            return View(model);
        }

        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Username","LastName","Email","Password","ConfirmPassword")]RegisterDto model){
            if(ModelState.IsValid){

            }
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}