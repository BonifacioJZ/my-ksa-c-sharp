using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Service;
using Domain.Dto.Category;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace WebApp.Controllers
{
        public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger,ICategoryService categoryService)
        {
            _logger = logger;
            this._categoryService = categoryService;
        }
        public async Task<ActionResult> Index(string search,string currentOrder, int? numPage,string currentFilter)
        {

            if(search != null)
                numPage = 1;
            else
                search=currentFilter;
        

            ViewData["OrdenActual"] = currentOrder;
            ViewData["FiltroActual"] = search;

            var categories = this._categoryService.GetAll(search,currentOrder);            
            
            return View(await Pagination<Category>.PaginationCreate(categories.AsNoTracking(),numPage??1,5));
        }
        public IActionResult New(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Store([Bind("Name","Description")]CategoryInDto category){
            if(ModelState.IsValid){
                var category_out = await this._categoryService.Create(category);
                if(category_out ==null){
                    TempData["Error_data"] ="El Intento de Registro no Valido";
                    return View(category);
                }
                TempData["Success_data"] = "La Categoria "+category_out.Name+ " se creo exitosamente";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error_data"] ="El Intento de Registro no Valido";
            return View("new",category);
        }

        public async Task<ActionResult> Show(Guid id){
            var category = await this._categoryService.Show(id);
            if(category == null){
                return NotFound();
            }

            return View(category); 
        }

        public async Task<ActionResult> Edit(Guid id){
            var category = await _categoryService.Edit(id);

            if(category==null){
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Guid id,[Bind("Id","Name","Description")]CategoryEditDto category){
            
            if(id!= category.Id) return NotFound();
            
            if(ModelState.IsValid){
                try{
                    await _categoryService.Update(category);
                
                }catch(DbUpdateConcurrencyException){
                    
                    if(!this._categoryService.Exist(id)){
                        return NotFound();
                    }else{
                        TempData["Error_data"] ="El Intento de Actualizacion no Valido";
                        throw ;
                    }
                }
                TempData["Success_data"]="La categoria se a actualizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error_data"] ="El Intento de Actualizacion no Valido";
            return View("edit",category);
        }

        public async Task<ActionResult> Delete(Guid id){
            
            
            var category = await this._categoryService.Show(id);

            if(category==null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Destroy(Guid id){
            _categoryService.Destroy(id);
            TempData["Success_data"]="La categoria se elimino correctamente";
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}