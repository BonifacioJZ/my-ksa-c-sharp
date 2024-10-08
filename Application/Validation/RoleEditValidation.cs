using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using FluentValidation;

namespace Application.Validation
{
    public class RoleEditValidation : AbstractValidator<Role>
    {
        public RoleEditValidation(){
            RuleFor(x=>x.Id).NotEmpty().NotNull();
            RuleFor(x=>x.Name).NotEmpty().NotNull().MaximumLength(150);
        }
    }
}