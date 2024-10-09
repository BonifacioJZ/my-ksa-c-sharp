using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Service;
using Domain.Dto.Role;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace WebApp.Controllers
{
    
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleServices _roleService;
        public RoleController(ILogger<RoleController> logger,IRoleServices roleService)
        {
            _roleService = roleService;
            _logger = logger;
        }
        
        public async Task<IActionResult> Index(int? numPage)
        {
            var roles = _roleService.GetAll();
            return View(await Pagination<Role>.PaginationCreate(roles.AsNoTracking(),numPage??1,10));
        }

        [Authorize(Roles ="Root")]
        public IActionResult New(){
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="Root")]
        public async Task<IActionResult> Store([Bind("Name","Description")]RoleInDto roleIn){
            if(ModelState.IsValid){
                
                var success = await _roleService.Save(roleIn);

                if(!success.Succeeded){
                    TempData["Error_data"] ="El Intento de Registro no Valido";
                    return View("new",roleIn);
                }

                TempData["Success_data"] = "El Role "+roleIn.Name + " fue creado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View("new",roleIn);
        }
        [Authorize(Roles ="Root")]
        public async Task<IActionResult> Show(Guid id){
            var role = await _roleService.Show(id);
            if(role ==null){
                return NotFound();
            }
            return View(role);
        }
        public async Task<IActionResult> Edit(Guid id){
            var role = await _roleService.Edit(id);
            if(role == null) return NotFound();
            return View(role);
        }

        public async Task<IActionResult> Update(Guid id, [Bind("Id","Name","Description")]RoleEditDto role){
            if(id != role.Id) return NotFound();
            
            if(ModelState.IsValid){
                try{
                    await _roleService.Update(role);
                }catch(DbUpdateConcurrencyException){
                    if(!_roleService.Exist(id)){
                        return NotFound();
                    }else{
                        TempData["Error_data"] ="El Intento de Actualizacion no Valido";
                        throw ;
                    }
                }
                TempData["Success_data"]="El Rol se a actualizado correctamente";
                return RedirectToAction(nameof(Index));

            }
            return View("edit",role);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}