using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Dto.Authentication;
using Domain.Dto.User;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Service
{
    public interface IUserService
    {
        Task<bool> ExistUserName(String Username);
        Task<bool> ExistEmail(String Email);
        Task<IdentityResult> Register(RegisterDto user);
        Task<SignInResult> LogIn(LoginDto user);
        IQueryable<User> GetAll();
        void LogOut();
        Task<UserDetailsDto> Show(Guid id);
        Task<UserEditDto> Found(Guid id);
        Task<IList<string>> GetRoleByUser(User user);
    }
}