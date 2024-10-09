using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Authentication;
using Domain.Dto.User;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly Context _context;
        public UserService(Context context,IMapper mapper, UserManager<User> userManager,SignInManager<User> signInManager){
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<bool> ExistEmail(string Email)
        {
            return  await _context.Users.Where(u=>u.Email == Email).AnyAsync();
        }

        public async Task<bool> ExistUserName(string Username)
        {
            return await _context.Users.Where(u=>u.UserName == Username).AnyAsync();
        }

        public IQueryable<User> GetAll()
        {
            var users = _userManager.Users.Select(c=>c);
            return users;
        }

        public async Task<SignInResult> LogIn(LoginDto user)
        {
            var result =  await _signInManager.PasswordSignInAsync(user.Username!,user.Password!,user.RememberMe,false);
            return result;
        }

        public async void LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterDto user)
        {
            User newUser = new(){
                FirstName = user.FirstName,
                LastNAme = user.LastName,
                UserName = user.Username,
                Email = user.Email,
            };
            var result = await _userManager.CreateAsync(newUser,user.Password!);
            return result;
        }

        public async Task<UserDetailsDto> Show(Guid id)
        {
            var user = _mapper.Map<UserDetailsDto>(await _userManager.FindByIdAsync(id.ToString()));
            return user;
        }
    }
}