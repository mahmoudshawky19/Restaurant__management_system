using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class InventoryController : Controller
    {
        private readonly IInventoryRepository inventoryRepository;
        private readonly ISupplierRepository supplierRepository;
        public InventoryController(IInventoryRepository inventoryRepository , ISupplierRepository supplierRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.supplierRepository = supplierRepository;
        }
        public IActionResult Index(string search = null, int page = 1)
        {
            var pageSize = 5; 
            var cs = inventoryRepository.GetAll([e => e.Supplier]);

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
            ViewBag.a = supplierRepository.GetAll();
             return View( );
        }
        [HttpPost]
        public IActionResult Create(Inventory inventory)
        {
            inventoryRepository.Add(inventory);
            inventoryRepository.Commit();
            TempData["success"] = "Add Inventory successfully";
             return RedirectToAction("");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.a = supplierRepository.GetAll();

            var inv = inventoryRepository.GetOne([], e=>e.Id == id).FirstOrDefault();
            return View(inv);
        }
        [HttpPost]
        public IActionResult Edit(Inventory inventory)
        {
            inventoryRepository.Edit(inventory);
            inventoryRepository.Commit();
            TempData["success"] = "Edit Inventory successfully";
            return RedirectToAction("");

        }
        public IActionResult Delete(int id)
        {
            Inventory inventory = new Inventory() { Id = id }; 
            inventoryRepository.Delete(inventory);
            inventoryRepository.Commit();
            TempData["success"] = "Delete Inventory successfully";
            return RedirectToAction("");
        }
    }
}
