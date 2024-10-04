using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Service;
using Domain.Dto.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        public IActionResult Index(){
            return View();
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
                
                var result = await _userService.Register(model);

                if(result.Succeeded){
                    return RedirectToAction("Index","Home");
                }
                foreach(var error in result.Errors){
                    ModelState.AddModelError("",error.Description);
                }

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