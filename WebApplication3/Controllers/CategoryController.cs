using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationContext context;
        public CategoryController(ApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await context.Categories.ToListAsync();
            return View(data);


        }
        public async Task<IActionResult> AddCategory(int? id)
        {
            Category category = new Category();
           if(id!=null && id != 0)
            {
                category = await context.Categories.FindAsync(id);
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
                if (category.CategoryId == 0)
                {
                    await context.Categories.AddAsync(category);
                   
                    TempData["success"] = "Category has been created";
                }
                else
                {
                    context.Categories.Update(category);
                    TempData["success"] = "Category has been created";
                }
                await context.SaveChangesAsync();



            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                bool status = context.Products.Any(x => x.CategoryId == id);
                if (status)
                {
                    TempData["warning"] = "Category is taken by another Product, so cant delete this";
                }
                else
                {
                    var category = await context.Categories.FindAsync(id);
                    if (category == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        context.Categories.Remove(category);
                        await context.SaveChangesAsync();
                        TempData["Success"] = "Category has been successfully deleted";

                    }
                }
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

    }
}
