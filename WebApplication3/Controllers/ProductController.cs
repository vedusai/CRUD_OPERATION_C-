using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Models.ViewModel;
using PagedList.Mvc;
using PagedList;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationContext context;
        public ProductController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int pg=1)

        {
            //Join
            var data = (from p in context.Products
                        join c in context.Categories
                        on p.CategoryId equals c.CategoryId
                        select new ProductCategorySummaryViewModel
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName
                        }).ToList();
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = data.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var datas = data.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(datas);
        }


        public async Task<IActionResult> AddProduct()
        {
            ViewBag.category = await context.Categories.ToListAsync();
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductCategorySummaryViewModel proCat)
        {
            ViewBag.category = await context.Categories.ToListAsync();
            try
            {
                ModelState.Remove("CategoryName");
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Please enter valid data!");
                    return View(proCat);
                }
                else
                {
                    var data = new Product()
                    {
                        ProductName = proCat.ProductName,
                        CategoryId = proCat.CategoryId,

                    };
                    await context.Products.AddAsync(data);
                    await context.SaveChangesAsync();
                    TempData["success"] = "Record has been inserted ";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var data = await context.Products.FindAsync(id);
                    if (data == null)
                    {
                        return NotFound(data);
                    }
                    else
                    {
                        context.Products.Remove(data);
                        await context.SaveChangesAsync();
                        TempData["Success"] = "Record has been successfully deleted";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DetailsProduct(int id)
        {

            ProductCategorySummaryViewModel productCategory = new ProductCategorySummaryViewModel();
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    productCategory = (from p in context.Products.Where(p => p.ProductId == id)
                                       join c in context.Categories
                                       on p.CategoryId equals c.CategoryId
                                       select new ProductCategorySummaryViewModel
                                       {
                                           ProductId = p.ProductId,
                                           ProductName = p.ProductName,
                                           CategoryId = c.CategoryId,
                                           CategoryName = c.CategoryName
                                       }).First();
                    if (productCategory == null)
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(productCategory);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            ViewBag.category = await context.Categories.ToListAsync();
            ProductCategorySummaryViewModel productCategory = new ProductCategorySummaryViewModel();
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    productCategory = (from p in context.Products.Where(p => p.ProductId == id)
                                       join c in context.Categories
                                       on p.CategoryId equals c.CategoryId
                                       select new ProductCategorySummaryViewModel
                                       {
                                           ProductId = p.ProductId,
                                           ProductName = p.ProductName,
                                           CategoryId = c.CategoryId,
                                           CategoryName = c.CategoryName
                                       }).First();
                    if (productCategory == null)
                    {
                        return NotFound();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View(productCategory);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductCategorySummaryViewModel proCat)
        {
            ViewBag.category = await context.Categories.ToListAsync();
            try
            {
                ModelState.Remove("CategoryName");
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Please enter valid data!");
                    return View(proCat);
                }
                else
                {
                    var data = new Product()
                    {
                        ProductId = proCat.ProductId,
                        ProductName = proCat.ProductName,
                        CategoryId = proCat.CategoryId,

                    };
                    context.Products.Update(data);
                    await context.SaveChangesAsync();
                    TempData["success"] = "Record has been updated ";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");

        }








    }
}
