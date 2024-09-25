using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Context:DbContext
    {
        public Context(DbContextOptions options):base(options){
        }
        public DbSet<Category> categories {get;set;} 

        //Todo(agregar automaticamente los timestamps)
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreateAt = now;
                }
                ((BaseEntity)entity.Entity).UpdateAt = now;
            }
        }
    }
}