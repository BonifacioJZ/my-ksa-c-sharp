using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Service
{
    public class RoleService : IRoleServices
    {
        private readonly RoleManager<Role> _roleManager;
        public RoleService(RoleManager<Role> roleManager){
            _roleManager = roleManager;
        }
        public IQueryable<Role> GetAll()
        {
            var roles = _roleManager.Roles.Select(c=>c);
            return roles;
        }
    }
}