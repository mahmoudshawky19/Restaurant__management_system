using Microsoft.AspNetCore.Mvc;
 using Models;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class TableController : Controller
    {
        private readonly ITableRepository tableRepository;
        public TableController(ITableRepository tableRepository)
        {
            this.tableRepository = tableRepository; 
        }
        public IActionResult Index(int page = 1, int pageSize = 5 ,string search = null)
        {
             var tables = tableRepository.GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                tables = tables.Where(e => e.tableNumber.ToString().Contains(search));

                if (!tables.Any())
                {
                    return RedirectToAction("NotFoundPage", "Home");
                }
            }

            var totalItems = tables.Count();

             var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

             var tablesOnPage = tables.Skip((page - 1) * pageSize).Take(pageSize).ToList();

             ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(tablesOnPage);   
        }

        public IActionResult Create()
        {
             return View ();
        }
        [HttpPost]
        public IActionResult Create(RestaurantTable table)
        {
            tableRepository.Add(table);
            tableRepository.Commit();
            TempData["success"] = "Add Table successfully"; 
            return RedirectToAction("");
        }
        public IActionResult Edit(int id)
        {
            var t = tableRepository.GetOne([], e => e.Id == id).FirstOrDefault();
            return View(t);
        }
        [HttpPost]
        public IActionResult Edit(RestaurantTable restaurantTable)
        {
            tableRepository.Edit(restaurantTable);
            tableRepository.Commit();
            TempData["success"] = "Edit Table successfully";
            return RedirectToAction("");

        }
        public IActionResult Delete(int id)
        {
            RestaurantTable table = new RestaurantTable() { Id = id }; 
            tableRepository.Delete(table);
            tableRepository.Commit();
            TempData["success"] = "Delete Table successfully";
            return RedirectToAction("");

        }
    }
}
