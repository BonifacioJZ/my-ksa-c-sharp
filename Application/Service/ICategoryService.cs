using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Category;
using Domain.Models;
using Persistence.Data;

namespace Application.Service
{
        public interface ICategoryService
    {
        IQueryable<Category> GetAll(string search,string currentOrder);
        Task<CategoryOutDto?> Create(CategoryInDto categoryInDto);
        Task<CategoryDetails?> Show(Guid id);
        Task<CategoryEditDto?> Edit(Guid Id);
        Task<CategoryOutDto?> Update(CategoryEditDto Category);
        bool Exist(Guid Id);
        void Destroy(Guid Id);
    }
}