using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Service;
using Domain.Models;
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

        public IActionResult Register(){
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}