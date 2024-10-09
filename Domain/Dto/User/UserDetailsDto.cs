using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.User
{
    public class UserDetailsDto
    {
        public Guid Id { get; }
        [Display(Name = "Nombre")]
        public string? FirstNAme { get; }
        [Display(Name ="Apellidos")]
        public string? LastNAme { get;}
        [Display(Name = "Nombre de Usuario")]
        public string? UserName { get; }
        [Display(Name ="Correo Electronico")]
        public string? Email { get; }
    }
}