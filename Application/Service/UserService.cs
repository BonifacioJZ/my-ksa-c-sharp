using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Service
{
    public class UserService : IUserService
    {

        private readonly Context _context;
        public UserService(Context context){
            _context = context;
        }
        public async Task<bool> ExistEmail(string Email)
        {
            return  await _context.Users.Where(u=>u.Email == Email).AnyAsync();
        }

        public async Task<bool> ExistUserName(string Username)
        {
            return await _context.Users.Where(u=>u.UserName == Username).AnyAsync();
        }
    }
}