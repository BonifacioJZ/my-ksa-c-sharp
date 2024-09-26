using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Authentication;
using FluentValidation;

namespace Application.Validation
{
    public class LoginValidation:AbstractValidator<LoginDto>
    {
        public LoginValidation(){
            RuleFor(x=>x.Username).NotEmpty()
            .NotNull();
            RuleFor(x=>x.Password).NotEmpty()
            .NotNull();
        }    
    }
}