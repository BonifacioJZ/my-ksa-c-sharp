using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Dto.Category
{
    public class CategoryDetails
    {
        public Guid Id {get;set;}
        public string Name {get;set;} ="name";
        public string? Description {get;set;}
    }
}