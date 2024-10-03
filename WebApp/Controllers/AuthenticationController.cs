using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Application.Service;
using Domain.Dto.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{

    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserService _userService;
        public AuthenticationController(ILogger<AuthenticationController> logger,IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Username","Password","RememberMe")]LoginDto model){
            if(ModelState.IsValid){
                var result = await _userService.LogIn(model);
                if(result.Succeeded){
                    return RedirectToAction("Index","Home");
                }
                TempData["Error_Data"]="Initento de Inicio de Sesión no valido";
                return View(model);
            }
            return View(model);
        }

        //CreateSuperUser
        public  IActionResult Logout(){
            _userService.LogOut();
            return RedirectToAction("Login","Authentication");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}