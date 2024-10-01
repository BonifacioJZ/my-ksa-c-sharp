using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IUserService
    {
        Task<bool> ExistUserName(String Username);
        Task<bool> ExistEmail(String Email);
    }
}