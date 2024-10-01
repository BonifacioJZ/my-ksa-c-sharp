using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Authentication;
using FluentValidation;
using Persistence;

namespace Application.Validation
{
    public class RegistrationValidation:AbstractValidator<RegisterDto>
    {
        
        public RegistrationValidation(){
            RuleFor(x=>x.FirstName).NotNull().NotEmpty()
            .MaximumLength(150);
            RuleFor(x =>x.LastName).NotNull().NotEmpty()
            .MaximumLength(150);   
        }
    }
}