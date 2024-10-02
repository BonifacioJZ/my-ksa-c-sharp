using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Role;
using FluentValidation;

namespace Application.Validation
{
    public class RoleValidation :AbstractValidator<RoleInDto>
    {
        public RoleValidation(){
            RuleFor(r=>r.Name).NotEmpty().MaximumLength(150).NotNull();
        }
    }
}