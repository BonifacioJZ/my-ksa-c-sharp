using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Context:IdentityDbContext<User>
    {
        public Context()
        {
        }

        public Context(DbContextOptions options):base(options){
        }
        public DbSet<Category> categories {get;set;} 
    }
} 