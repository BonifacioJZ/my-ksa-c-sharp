using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        [MaxLength(150)]
        public string? FirstName {get;set;}
        [MaxLength(150)]
        public string? LastNAme {get;set;}        
    }
}