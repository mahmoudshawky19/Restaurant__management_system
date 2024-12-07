using Microsoft.AspNetCore.Mvc;
using Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IActionResult Index(string search = null, int page = 1)
        {
            var pageSize = 5;
            var customers = customerRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                customers = customers.Where(e => e.ZipCode.Contains(search));

                if (!customers.Any())
                {
                    ViewBag.ErrorMessage = "No customers found with that ZipCode.";
                }
            }

            var totalItems = customers.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            var customersOnPage = customers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(customersOnPage);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Add(customer);
                customerRepository.Commit();
                TempData["success"] = "Add Customer successfully";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = customerRepository.GetOne([],e=>e.Id==id).FirstOrDefault();
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Edit(customer);
                customerRepository.Commit();
                TempData["success"] = "Edit Customer successfully";
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = new Customer() { Id = id };
            customerRepository.Delete(customer);
            customerRepository.Commit();
            TempData["success"] = "Delete Customer successfully";
            return RedirectToAction("Index");
        }
    }
}
