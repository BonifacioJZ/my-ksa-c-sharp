using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.User;
using Domain.Models;

namespace Application.Mapper
{
    public class ProfileUser : Profile
    {
        public ProfileUser(){
            CreateMap<User,UserDetailsDto>();
            CreateMap<UserDetailsDto,User>();
        }
    }
}