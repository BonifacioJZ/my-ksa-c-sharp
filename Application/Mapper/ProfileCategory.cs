using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Category;
using Domain.Models;

namespace Application.Mapper
{
    public class ProfileCategory:Profile
    {
        public ProfileCategory(){
            CreateMap<CategoryOutDto,Category>();
            CreateMap<CategoryInDto,Category>();
            CreateMap<Category,CategoryOutDto>();
            CreateMap<Category,CategoryInDto>();
            CreateMap<CategoryDetails,Category>();
            CreateMap<Category,CategoryDetails>();
            CreateMap<CategoryEditDto,Category>();
            CreateMap<Category,CategoryEditDto>();
        }
    }
}