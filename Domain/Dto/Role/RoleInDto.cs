using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.Role
{
    public class RoleInDto
    {
        [Required]
        [StringLength(maximumLength:150)]
        public string? Name {get; set;}
    }
}