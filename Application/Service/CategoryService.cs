using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Category;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Application.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public CategoryService(Context context,IMapper mapper){
            this._context=context;
            this._mapper = mapper;
        }
        public async Task<CategoryOutDto?> Create(CategoryInDto categoryInDto)
        {
            var category = _mapper.Map<Category>(categoryInDto);
            category.Id = Guid.NewGuid();
            this._context.Add(category);
            var result = await _context.SaveChangesAsync();
            if(result==0) return null;
            var category_out = _mapper.Map<CategoryOutDto>(category);
            return category_out;
        }

        public async void Destroy(Guid id)
        {
            var category = await  this.Found(id);
            if(category != null){
                _context.Remove(category);
            }
            await _context.SaveChangesAsync();
            
        }
        public async Task<CategoryEditDto?> Edit(Guid id)
        {
            var category = _mapper.Map<CategoryEditDto>(await this.Found(id));
            return category;
        }

        public bool Exist(Guid Id)
        {
            return(_context.categories?.Any(c=>c.Id== Id)).GetValueOrDefault();
        }

        //
        public IQueryable<Category> GetAll(string search, string currentOrder)
        {
            var categories = _context.categories
            .Select(c=>c);
            if(!String.IsNullOrEmpty(search)){
                categories = categories.Where(
                    c=>c.Name.Contains(search)
                );
            }
            
            return categories;
        }


        public async Task<CategoryDetails?> Show(Guid id)
        {
            var category = _mapper.Map<CategoryDetails>(await this.Found(id));
            return category;
        }

        public async Task<CategoryOutDto?> Update(CategoryEditDto category)
        {
            var newCategory = _mapper.Map<Category>(category);
            _context.Update(newCategory);
            var result = await _context.SaveChangesAsync();
            
            if (result==0) return null;
            var categoryOut = _mapper.Map<CategoryOutDto>(newCategory);
            
            return categoryOut;
        }

        private async Task<Category?> Found(Guid Id){
            var category = await _context.categories.FirstOrDefaultAsync(
                c=> c.Id == Id
            );
            return category;
        } 
    }
}