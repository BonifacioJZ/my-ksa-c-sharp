using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.Role
{
    public class RoleDetails
    {
        [Display(Name ="Nombre")]
        public string? Name { get; set; }
        [Display(Name ="Descripcion")]
        public string? Description { get; set; }
    }
}