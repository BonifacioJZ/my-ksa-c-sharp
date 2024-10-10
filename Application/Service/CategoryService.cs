using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dto.Category;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;

namespace Application.Service;

/// <summary>
/// Servicio para gestionar las operaciones relacionadas con las categorías en la base de datos.
/// Implementa la interfaz <see cref="ICategoryService"/>.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Constructor del servicio de categorías.
    /// </summary>
    /// <param name="context">El contexto de la base de datos.</param>
    /// <param name="mapper">El objeto <see cref="IMapper"/> para mapear entre entidades y DTOs.</param>
    public CategoryService(Context context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    /// <summary>
    /// Crea una nueva categoría en la base de datos.
    /// </summary>
    /// <param name="categoryInDto">El DTO de entrada que contiene los datos de la nueva categoría.</param>
    /// <returns>
    /// Un objeto <see cref="CategoryOutDto"/> con los detalles de la categoría creada, o <c>null</c> si la creación falla.
    /// </returns>
    public async Task<CategoryOutDto?> Create(CategoryInDto categoryInDto)
    {
        var category = _mapper.Map<Category>(categoryInDto);
        category.Id = Guid.NewGuid();
        this._context.Add(category);
        var result = await _context.SaveChangesAsync();
        if(result == 0) return null;
        var category_out = _mapper.Map<CategoryOutDto>(category);
        return category_out;
    }

    /// <summary>
    /// Elimina una categoría de la base de datos basada en su identificador.
    /// </summary>
    /// <param name="id">El identificador único de la categoría.</param>
    public async void Destroy(Guid id)
    {
        var category = await this.Found(id);
        if (category != null)
        {
            _context.Remove(category);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Edita una categoría existente y devuelve los detalles editados.
    /// </summary>
    /// <param name="id">El identificador único de la categoría a editar.</param>
    /// <returns>
    /// Un objeto <see cref="CategoryEditDto"/> con los detalles de la categoría para editar, o <c>null</c> si no se encuentra.
    /// </returns>
    public async Task<CategoryEditDto?> Edit(Guid id)
    {
        var category = _mapper.Map<CategoryEditDto>(await this.Found(id));
        return category;
    }

    /// <summary>
    /// Verifica si una categoría existe en la base de datos.
    /// </summary>
    /// <param name="id">El identificador único de la categoría.</param>
    /// <returns>
    /// <c>true</c> si la categoría existe, <c>false</c> en caso contrario.
    /// </returns>
    public bool Exist(Guid id)
    {
        return (_context.categories?.Any(c => c.Id == id)).GetValueOrDefault();
    }

    /// <summary>
    /// Obtiene todas las categorías que coinciden con una búsqueda, con opción de ordenar.
    /// </summary>
    /// <param name="search">El término de búsqueda para filtrar las categorías.</param>
    /// <param name="currentOrder">El criterio de ordenación actual.</param>
    /// <returns>
    /// Un <see cref="IQueryable{Category}"/> con las categorías que coinciden con la búsqueda.
    /// </returns>
    public IQueryable<Category> GetAll(string search, string currentOrder)
    {
        var categories = _context.categories.Select(c => c);
        if (!string.IsNullOrEmpty(search))
        {
            categories = categories.Where(c => c.Name.Contains(search));
        }
        return categories;
    }

    /// <summary>
    /// Muestra los detalles de una categoría específica.
    /// </summary>
    /// <param name="id">El identificador único de la categoría.</param>
    /// <returns>
    /// Un objeto <see cref="CategoryDetails"/> con los detalles de la categoría, o <c>null</c> si no se encuentra.
    /// </returns>
    public async Task<CategoryDetails?> Show(Guid id)
    {
        var category = _mapper.Map<CategoryDetails>(await this.Found(id));
        return category;
    }

    /// <summary>
    /// Actualiza una categoría existente en la base de datos.
    /// </summary>
    /// <param name="category">El objeto <see cref="CategoryEditDto"/> con los datos actualizados de la categoría.</param>
    /// <returns>
    /// Un objeto <see cref="CategoryOutDto"/> con los detalles de la categoría actualizada, o <c>null</c> si la actualización falla.
    /// </returns>
    public async Task<CategoryOutDto?> Update(CategoryEditDto category)
    {
        var newCategory = _mapper.Map<Category>(category);
        _context.Update(newCategory);
        var result = await _context.SaveChangesAsync();
        
        if (result == 0) return null;
        var categoryOut = _mapper.Map<CategoryOutDto>(newCategory);
        
        return categoryOut;
    }

    /// <summary>
    /// Busca una categoría por su identificador en la base de datos.
    /// </summary>
    /// <param name="id">El identificador único de la categoría.</param>
    /// <returns>
    /// Un objeto <see cref="Category"/> si la categoría es encontrada, o <c>null</c> si no existe.
    /// </returns>
    private async Task<Category?> Found(Guid id)
    {
        var category = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }
}

