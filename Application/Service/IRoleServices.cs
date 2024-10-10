using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Role;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Service
{
    public interface IRoleServices
    {
        IQueryable<Role> GetAll();
        Task<ICollection<RoleOutDto>> GetAllDto();
        Task<IdentityResult> Save(RoleInDto roleIn);
        Task<RoleDetails> Show(Guid id);
        Task<RoleEditDto?> Edit(Guid id);
        Task<IdentityResult?> Update(RoleEditDto role);
        bool Exist(Guid id);
        void Destroy(Guid id);
    }
}