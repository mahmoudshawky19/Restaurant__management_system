using Microsoft.AspNetCore.Mvc;
using  Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class SupplierController : Controller
    {
        private readonly ISupplierRepository supplierRepository;
        public SupplierController(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;   
        }
        public IActionResult Index(string search = null, int page = 1, int pageSize = 5)
        {
             var cs = supplierRepository.GetAll();

             if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                cs = cs.Where(e => e.Name.Contains(search));
            }

             var totalItems = cs.Count();

             var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

             var suppliersOnPage = cs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

             ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(suppliersOnPage);  
        }
        public IActionResult Create()
        {
             return View(  );
        }
        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            supplierRepository.Add(supplier);
            supplierRepository.Commit();
            TempData["success"] = "Add Supplier Successfully"; 
            return RedirectToAction("");
        }
        public IActionResult Edit(int id)
        {
            var s = supplierRepository.GetOne([], e => e.Id == id).FirstOrDefault();

            return View(s);
        }
        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            supplierRepository.Edit(supplier);
            supplierRepository.Commit();
            TempData["success"] = "Edit Supplier Successfully";

            return RedirectToAction("");
        }
        public IActionResult Delete(int id)
        {
            Supplier supplier = new Supplier() { Id = id };
            supplierRepository.Delete(supplier);
            supplierRepository.Commit();
            TempData["success"] = "Delete Supplier Successfully";
            return RedirectToAction("");

        }
    }
}
