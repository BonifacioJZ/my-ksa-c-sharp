using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Application.Service;
using Domain.Dto.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    //TODO("Validar si usuario esta authenticado redirecionar login")
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        public AuthenticationController(ILogger<AuthenticationController> logger,IUserService userService,UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            _userService = userService;
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
        public async Task<IActionResult> Register([Bind("FirstName","LastName","Username","Email","Password","ConfirmPassword")]RegisterDto model){
            if(ModelState.IsValid){

                if(await _userService.ExistUserName(model.Username!)){
                    ModelState.AddModelError("Username","El Nombre de usuario ya existe");
                    return View(model);
                }
                if(await _userService.ExistEmail(model.Email!)){
                    ModelState.AddModelError("Email","El Correo electronico ya existe");
                    return View(model);
                }
                User user = new(){
                    FirstName = model.FirstName,
                    LastNAme = model.LastName,
                    UserName = model.Username,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user,model.Password!);
                if(result.Succeeded){
                    await _signInManager.SignInAsync(user,false);
                    return RedirectToAction("Index","Home");
                }
                foreach(var error in result.Errors){
                    ModelState.AddModelError("",error.Description);
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout(){
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Authentication");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}