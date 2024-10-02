using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Service
{
    public interface IUserService
    {
        Task<bool> ExistUserName(String Username);
        Task<bool> ExistEmail(String Email);
        Task<IdentityResult> Register(RegisterDto user);
        Task<SignInResult> LogIn(LoginDto user);
        void LogOut();
    }
}