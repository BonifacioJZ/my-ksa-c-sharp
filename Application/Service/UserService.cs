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

namespace Application.Service;
    /// <summary>
/// Servicio para gestionar operaciones relacionadas con los usuarios.
/// Implementa la interfaz <see cref="IUserService"/>.
/// </summary>
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRoleServices _roleServices;
    private readonly IMapper _mapper;
    private readonly Context _context;

    /// <summary>
    /// Constructor del servicio de usuarios.
    /// </summary>
    /// <param name="context">El contexto de la base de datos.</param>
    /// <param name="mapper">El objeto <see cref="IMapper"/> para mapear entre entidades y DTOs.</param>
    /// <param name="userManager">El administrador de usuarios para realizar operaciones relacionadas con los usuarios.</param>
    /// <param name="signInManager">El administrador de inicio de sesión para realizar operaciones relacionadas con la autenticación de usuarios.</param>
    public UserService(Context context, IMapper mapper, UserManager<User> userManager, IRoleServices roleServices,SignInManager<User> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _roleServices = roleServices;
    }

    /// <summary>
    /// Verifica si un correo electrónico ya está registrado en la base de datos.
    /// </summary>
    /// <param name="Email">El correo electrónico que se desea verificar.</param>
    /// <returns><c>true</c> si el correo electrónico ya existe, <c>false</c> en caso contrario.</returns>
    public async Task<bool> ExistEmail(string Email)
    {
        return await _context.Users.Where(u => u.Email == Email).AnyAsync();
    }

    /// <summary>
    /// Verifica si un nombre de usuario ya está registrado en la base de datos.
    /// </summary>
    /// <param name="Username">El nombre de usuario que se desea verificar.</param>
    /// <returns><c>true</c> si el nombre de usuario ya existe, <c>false</c> en caso contrario.</returns>
    public async Task<bool> ExistUserName(string Username)
    {
        return await _context.Users.Where(u => u.UserName == Username).AnyAsync();
    }

    public async Task<UserEditDto> Found(Guid id)
    {
        var user = _mapper.Map<UserEditDto>(await _userManager.FindByIdAsync(id.ToString()));
        return user;
    }

    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    /// <returns>Un <see cref="IQueryable{User}"/> que contiene todos los usuarios.</returns>
    public IQueryable<User> GetAll()
    {
        var users = _userManager.Users.Select(c => c);
        return users;
    }

    public async Task<IList<string>> GetRoleByUser(User user)
    {
        var role = await _userManager.GetRolesAsync(user);
        return role;
    }

    /// <summary>
    /// Inicia sesión en la aplicación con las credenciales proporcionadas.
    /// </summary>
    /// <param name="user">El objeto <see cref="LoginDto"/> que contiene las credenciales del usuario.</param>
    /// <returns>Un objeto <see cref="SignInResult"/> que indica si el inicio de sesión fue exitoso o no.</returns>
    public async Task<SignInResult> LogIn(LoginDto user)
    {
        var result = await _signInManager.PasswordSignInAsync(user.Username!, user.Password!, user.RememberMe, false);
        return result;
    }

    /// <summary>
    /// Cierra la sesión del usuario actualmente autenticado.
    /// </summary>
    public async void LogOut()
    {
        await _signInManager.SignOutAsync();
    }

    /// <summary>
    /// Registra un nuevo usuario en la aplicación.
    /// </summary>
    /// <param name="user">El objeto <see cref="RegisterDto"/> que contiene la información del nuevo usuario.</param>
    /// <returns>Un objeto <see cref="IdentityResult"/> que indica si el registro fue exitoso o no.</returns>
    public async Task<IdentityResult> Register(RegisterDto user)
    {
        User newUser = new()
        {
            FirstName = user.FirstName,
            LastNAme = user.LastName,
            UserName = user.Username,
            Email = user.Email,
        };
        var result = await _userManager.CreateAsync(newUser, user.Password!);
        if(!result.Succeeded) return result;
        result = await _userManager.AddToRoleAsync(newUser,user.Role);
        return result;
    }

    /// <summary>
    /// Muestra los detalles de un usuario basado en su identificador.
    /// </summary>
    /// <param name="id">El identificador único del usuario.</param>
    /// <returns>Un objeto <see cref="UserDetailsDto"/> con los detalles del usuario.</returns>
    public async Task<UserDetailsDto> Show(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var userDetails = _mapper.Map<UserDetailsDto>(user);
        userDetails.Role = await GetRoleByUser(user);
        return userDetails;
    }
}

