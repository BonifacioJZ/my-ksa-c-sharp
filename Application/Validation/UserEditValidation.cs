using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.User;
using FluentValidation;

namespace Application.Validation
{
    public class UserEditValidation : AbstractValidator<UserEditDto>
    {
        public UserEditValidation(){
            RuleFor(x=>x.FirstName).NotEmpty().NotNull()
            .MaximumLength(150);
            RuleFor(x=>x.LastName).MaximumLength(150).NotEmpty()
            .NotNull();
            RuleFor(x=>x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x=>x.Username).NotEmpty().NotNull().MaximumLength(36);
        }
    }
}