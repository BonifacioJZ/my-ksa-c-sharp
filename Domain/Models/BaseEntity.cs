using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity
    {
        [Column(TypeName ="create_at")]
        public DateTime? CreateAt {get;set;}
        [Column(TypeName ="update_at")]
        public DateTime? UpdateAt {get;set;}

    }
}