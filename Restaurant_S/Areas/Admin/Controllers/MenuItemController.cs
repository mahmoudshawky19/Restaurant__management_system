using Microsoft.AspNetCore.Mvc;
using Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class MenuItemController : Controller
    {
        private readonly IMenuItemRepository menuItemRepository;
        private readonly ICategoryRepository categoryRepository;
        public MenuItemController(IMenuItemRepository menuItemRepository , ICategoryRepository categoryRepository)
        {
            this.menuItemRepository = menuItemRepository;
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index(string search = null, int page = 1)
        {
            var pageSize = 5; 
            var cs = menuItemRepository.GetAll([e => e.Category] , e=>e.PriceBefore==0);

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

             var itemsOnPage = cs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

             ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;

            return View(itemsOnPage);
        }
        public IActionResult Create()
        {
            ViewBag.a = categoryRepository.GetAll().ToList();
             return View();
        }
        [HttpPost]
        public IActionResult Create(MenuItem menuItem, IFormFile ImgUrl)
        {
            if (ImgUrl != null && ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }

                menuItem.ImgUrl = fileName;
            }

            menuItemRepository.Add(menuItem);
            menuItemRepository.Commit();
            TempData["success"] = "Create menuitem Successfully";

            return RedirectToAction("");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.a = categoryRepository.GetAll().ToList();

            var m = menuItemRepository.GetOne([], e => e.Id == id).FirstOrDefault(); 
            return View(m);
        }
        [HttpPost]
        public IActionResult Edit(MenuItem menuItem , IFormFile ImgUrl)
        {
            var oldproduct = menuItemRepository.GetOne([], e => e.Id == menuItem.Id, false).FirstOrDefault();

            if (oldproduct == null)
            {
                return RedirectToAction("");
            }
            if (ImgUrl != null && ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);
                var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", oldproduct.ImgUrl);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }
                if (System.IO.File.Exists(oldfilePath))
                {
                    System.IO.File.Delete(oldfilePath);
                }
                menuItem.ImgUrl = fileName;
            }
            else
            {
                menuItem.ImgUrl = oldproduct.ImgUrl;
            }

            menuItemRepository.Edit(menuItem);
            menuItemRepository.Commit();
            TempData["success"] = "Edited menuItem successfuly";
            return RedirectToAction("");

        }
        public IActionResult Delete(int id)
        {
            MenuItem menuItem = new MenuItem() { Id = id };
            menuItemRepository.Delete(menuItem);
            menuItemRepository.Commit();
            TempData["success"] = "Delete menuitem successfully";
            return RedirectToAction("");
        }
     }
}
