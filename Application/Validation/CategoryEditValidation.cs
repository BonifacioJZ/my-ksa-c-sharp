using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Category;
using FluentValidation;

namespace Application.Validation
{
    public class CategoryEditValidation : AbstractValidator<CategoryEditDto>
    {
        public CategoryEditValidation(){
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).MaximumLength(150);
        }
    }
}