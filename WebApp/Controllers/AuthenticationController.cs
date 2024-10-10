using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Application.Service;
using Domain.Dto.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers;
/// <summary>
/// Controlador que gestiona la autenticación de los usuarios.
/// </summary>
public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor del controlador de autenticación.
    /// </summary>
    /// <param name="logger">El registrador de logs <see cref="ILogger{TCategoryName}"/> para registrar información sobre el controlador.</param>
    /// <param name="userService">El servicio de usuarios <see cref="IUserService"/> para manejar las operaciones relacionadas con la autenticación.</param>
    public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Muestra la vista de inicio de sesión.
    /// </summary>
    /// <returns>La vista de inicio de sesión.</returns>
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Maneja la lógica de inicio de sesión cuando se envían las credenciales de usuario.
    /// </summary>
    /// <param name="model">El objeto <see cref="LoginDto"/> que contiene el nombre de usuario, la contraseña y la opción de recordar la sesión.</param>
    /// <returns>Redirige al usuario a la página de inicio si el inicio de sesión es exitoso; en caso contrario, devuelve la vista de inicio de sesión con un mensaje de error.</returns>
    [HttpPost]
    public async Task<IActionResult> Login([Bind("Username,Password,RememberMe")] LoginDto model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.LogIn(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["Error_Data"] = "Intento de inicio de sesión no válido";
            return View(model);
        }
        return View(model);
    }

    /// <summary>
    /// Cierra la sesión del usuario autenticado y lo redirige a la vista de inicio de sesión.
    /// </summary>
    /// <returns>Redirige a la acción <c>Login</c> del controlador <c>Authentication</c>.</returns>
    public IActionResult Logout()
    {
        _userService.LogOut();
        return RedirectToAction("Login", "Authentication");
    }

    /// <summary>
    /// Muestra una página de error en caso de que ocurra un problema durante la autenticación.
    /// </summary>
    /// <returns>Devuelve una vista con un mensaje de error.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}

