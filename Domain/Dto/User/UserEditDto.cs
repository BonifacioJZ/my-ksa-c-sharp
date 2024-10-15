using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.User;

    public class UserEditDto
    {

        public Guid Id { get; set; }
        [Required]
        [StringLength(maximumLength:150)]
        [Display(Name ="Nombre")]
        public string? FirstName {get; set;}
        [Required]
        [StringLength(maximumLength:150)]
        [Display(Name ="Apellidos")]
        public string? LastName {get; set;}
        [Required]
        [Display(Name ="Nombre de Ususario")]
        public string? Username {get; set;}
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Correo Electronico")]
        public string? Email {get; set;}
    }
