using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Role;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class RoleService : IRoleServices
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<Role> roleManager,IMapper mapper){
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<RoleEditDto?> Found(Guid id)
        {
            var role = _mapper.Map<RoleEditDto>(await _roleManager.FindByIdAsync(id.ToString()));
            return role;
        }

        public IQueryable<Role> GetAll()
        {
            var roles = _roleManager.Roles.Select(c=>c);
            return roles;
        }

        public async Task<IdentityResult> Save(RoleInDto roleIn)
        {
            var role = new Role(){
                Name = roleIn.Name,
                Description = roleIn.Description,
                NormalizedName = roleIn.Name!.Normalize()
            };
            return await _roleManager.CreateAsync(role);
        }

        public async Task<RoleDetails> Show(Guid id)
        {
            var role = _mapper.Map<RoleDetails>(await _roleManager.FindByIdAsync(id.ToString()));
            return role;
        }
    }
}