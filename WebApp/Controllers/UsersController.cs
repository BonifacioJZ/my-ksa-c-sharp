using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Service;
using Domain.Dto.Authentication;
using Domain.Models;
using Domain.ModelView.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        private readonly IRoleServices _roleServices;
        public UsersController(ILogger<UsersController> logger,IRoleServices roleServices,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _roleServices = roleServices;
        }


        public async Task<IActionResult> Index(int? numPage){
            var users = _userService.GetAll();
            return View(await Pagination<User>.PaginationCreate(users.AsNoTracking(),numPage??1,10));
        }
        public async Task<IActionResult> Register(){
            RegisterModelView register = new RegisterModelView();
            register.Role = await _roleServices.GetAllDto();
            return View(register);
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("FirstName","LastName","Username","Email","Password","ConfirmPassword","Role")]RegisterDto model){
            if(ModelState.IsValid){
                if(model.Role=="Selecione un Rol"){
                    ModelState.AddModelError("Role","Selecione un Rol");
                    
                    return View(await ConverterToRegisterModelView(model));
                }
                if(await _userService.ExistUserName(model.Username!)){
                    ModelState.AddModelError("Username","El Nombre de usuario ya existe");
                    return View(await ConverterToRegisterModelView(model));
                }
                if(await _userService.ExistEmail(model.Email!)){
                    ModelState.AddModelError("Email","El Correo electronico ya existe");
                    return View(await ConverterToRegisterModelView(model));
                }
                
                var result = await _userService.Register(model);

                if(result.Succeeded){
                    return RedirectToAction("Index","Users");
                }
                foreach(var error in result.Errors){
                    ModelState.AddModelError("",error.Description);
                }

            }
            return View(await ConverterToRegisterModelView(model));
        }

        
        public async Task<IActionResult> Show(Guid id){
            var user  = await _userService.Show(id);
            if(user == null) return NotFound();
            return View(user);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        private async Task<RegisterModelView> ConverterToRegisterModelView(RegisterDto user){
            RegisterModelView model = new RegisterModelView(){
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Role = await _roleServices.GetAllDto(),
            };
            return model;
        }
    }
}