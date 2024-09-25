using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Category;
using FluentValidation;

namespace Application.Validation
{
    public class CategoryValidation :AbstractValidator<CategoryInDto>
    {
        public CategoryValidation(){
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).MaximumLength(150);
        }
    }
    
}