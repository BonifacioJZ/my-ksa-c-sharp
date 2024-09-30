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
            RuleFor(x=>x.Username).NotNull().NotEmpty().Must(IsUniqueUsername!).WithMessage("El Nombre de usuario ya existe");
            RuleFor(x=>x.Email).EmailAddress().NotEmpty().NotNull().Must(IsUniqueEmail).WithMessage("El Correo electronico ya esta en uso");    
        }
        private bool IsUniqueEmail(RegisterDto register,string email){
            return new Context().Users.Where(u=>u.Email == email)==null;
        }
        private bool IsUniqueUsername(RegisterDto register,string username){
            return new Context().Users.Where(x=>x.UserName!.ToLower()==username.ToLower())==null;
        }
    }
}