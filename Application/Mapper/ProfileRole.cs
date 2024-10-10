using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Role;
using Domain.Models;

namespace Application.Mapper
{
    public class ProfileRole : Profile
    {
        public ProfileRole(){
            CreateMap<Role,RoleDetails>();
            CreateMap<RoleDetails,Role>();
            CreateMap<RoleEditDto,Role>();
            CreateMap<Role,RoleEditDto>();
            CreateMap<Role,RoleOutDto>();
            CreateMap<RoleOutDto,Role>();
        }
    }
}