using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Role;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Service;
    /// <summary>
/// Servicio para gestionar roles utilizando <see cref="RoleManager{Role}"/>. Implementa la interfaz <see cref="IRoleServices"/>.
/// </summary>
public class RoleService : IRoleServices
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor del servicio de roles.
    /// </summary>
    /// <param name="roleManager">El administrador de roles <see cref="RoleManager{Role}"/> que gestiona las operaciones sobre roles.</param>
    /// <param name="mapper">El objeto <see cref="IMapper"/> para mapear entre entidades y DTOs.</param>
    public RoleService(RoleManager<Role> roleManager, IMapper mapper)
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Elimina un rol basado en su identificador.
    /// </summary>
    /// <param name="id">El identificador único del rol a eliminar.</param>
    public async void Destroy(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
    }

    /// <summary>
    /// Edita un rol y devuelve los detalles editados.
    /// </summary>
    /// <param name="id">El identificador único del rol a editar.</param>
    /// <returns>Un objeto <see cref="RoleEditDto"/> con los detalles del rol para editar, o <c>null</c> si no se encuentra.</returns>
    public async Task<RoleEditDto?> Edit(Guid id)
    {
        var role = _mapper.Map<RoleEditDto>(await _roleManager.FindByIdAsync(id.ToString()));
        return role;
    }

    /// <summary>
    /// Verifica si un rol existe basado en su identificador.
    /// </summary>
    /// <param name="id">El identificador único del rol.</param>
    /// <returns><c>true</c> si el rol existe, <c>false</c> en caso contrario.</returns>
    public bool Exist(Guid id)
    {
        return (_roleManager.Roles?.Any(r => r.Id == id.ToString())).GetValueOrDefault();
    }

    /// <summary>
    /// Obtiene todos los roles.
    /// </summary>
    /// <returns>Un <see cref="IQueryable{Role}"/> con todos los roles.</returns>
    public IQueryable<Role> GetAll()
    {
        var roles = _roleManager.Roles.Select(c => c);
        return roles;
    }

    /// <summary>
    /// Obtiene todos los roles como una colección de DTOs.
    /// Excluye el rol "Root" de los resultados.
    /// </summary>
    /// <returns>Una colección de objetos <see cref="RoleOutDto"/> que representan los roles.</returns>
    public async Task<ICollection<RoleOutDto>> GetAllDto()
    {
        var roles = _mapper.Map<ICollection<RoleOutDto>>(await _roleManager.Roles
            .Where(r => r.Name != "Root").ToListAsync());
        return roles;
    }

    /// <summary>
    /// Guarda un nuevo rol en la base de datos.
    /// </summary>
    /// <param name="roleIn">El objeto <see cref="RoleInDto"/> con los detalles del nuevo rol.</param>
    /// <returns>Un objeto <see cref="IdentityResult"/> que representa el resultado de la operación de creación.</returns>
    public async Task<IdentityResult> Save(RoleInDto roleIn)
    {
        var role = new Role()
        {
            Name = roleIn.Name,
            Description = roleIn.Description,
            NormalizedName = roleIn.Name!.Normalize()
        };
        return await _roleManager.CreateAsync(role);
    }

    /// <summary>
    /// Muestra los detalles de un rol específico basado en su identificador.
    /// </summary>
    /// <param name="id">El identificador único del rol.</param>
    /// <returns>Un objeto <see cref="RoleDetails"/> con los detalles del rol.</returns>
    public async Task<RoleDetails> Show(Guid id)
    {
        var role = _mapper.Map<RoleDetails>(await _roleManager.FindByIdAsync(id.ToString()));
        return role;
    }

    /// <summary>
    /// Actualiza un rol existente en la base de datos.
    /// </summary>
    /// <param name="role">El objeto <see cref="RoleEditDto"/> con los datos del rol actualizado.</param>
    /// <returns>Un objeto <see cref="IdentityResult"/> que representa el resultado de la operación de actualización.</returns>
    public async Task<IdentityResult?> Update(RoleEditDto role)
    {
        var updateRole = new Role()
        {
            Id = role.Id.ToString(),
            Name = role.Name,
            Description = role.Description,
            NormalizedName = role.Name!.Normalize()
        };
        var result = await _roleManager.UpdateAsync(updateRole);
        return result;
    }
}
