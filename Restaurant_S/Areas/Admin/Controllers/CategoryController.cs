using Microsoft.AspNetCore.Mvc;
using Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index(string search = null, int page = 1)
        {
            var pageSize = 5;   
            var cs = categoryRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                cs = cs.Where(e => e.Name.Contains(search));

                if (!cs.Any())
                {
                    return RedirectToAction("NotFoundPage", "Home");
                }
            }

            var totalItems = cs.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

             var categoriesOnPage = cs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

             ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(categoriesOnPage);
        }

        public IActionResult Create()
        {
             return View( );
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            categoryRepository.Add(category);
            categoryRepository.Commit();
            TempData["success"] = "Add Category successfully";
            return RedirectToAction(""); 
        }
        public IActionResult Edit(int id)
        {
           var ct =  categoryRepository.GetOne(expression: E => E.Id == id).FirstOrDefault();
            return View(ct);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            categoryRepository.Edit(category);
            categoryRepository.Commit();
            TempData["success"] = "Edit Category successfully";

            return RedirectToAction("");
        }
        public IActionResult Delete(int id)
        {
            Category category = new Category() { Id = id };
            categoryRepository.Delete(category);
            categoryRepository.Commit();
            TempData["success"] = "Delete Category successfully";
            return RedirectToAction("");
        }
    }
}
