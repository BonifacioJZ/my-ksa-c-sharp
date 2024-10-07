using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Service
{
    public interface IRoleServices
    {
        IQueryable<Role> GetAll();
    }
}