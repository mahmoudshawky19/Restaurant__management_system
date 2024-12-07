using Microsoft.AspNetCore.Mvc;
using  Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public IActionResult Index(string search = null, int page = 1)
        {
            var pageSize = 5;  
            var cs = employeeRepository.GetAll();

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

             var employeesOnPage = cs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

             ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;

            return View(employeesOnPage);
        }
        public IActionResult Create()
        {
             return View( );
        }
        [HttpPost]
        
        public IActionResult Create(Employee  employee , IFormFile ProfilePicture)
        {
            if (ProfilePicture != null && ProfilePicture.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ProfilePicture.CopyTo(stream);
                }

                employee.ProfilePicture = fileName;
            }

            employeeRepository.Add(employee);
            employeeRepository.Commit();
            TempData["success"] = "Create Employee Successfully";

            return RedirectToAction(""); 
        }
        public IActionResult Edit(int id)
        {
            var e = employeeRepository.GetOne([], e => e.Id == id).FirstOrDefault();
            return View(e);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee , IFormFile ProfilePicture)
        {
            var oldproduct = employeeRepository.GetOne([], e => e.Id == employee.Id, false).FirstOrDefault();
            
                if (oldproduct == null)
                {
                    return RedirectToAction("");
                }
                if (ProfilePicture != null && ProfilePicture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", fileName);
                    var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", oldproduct.ProfilePicture);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                    ProfilePicture.CopyTo(stream);
                    }
                    if (System.IO.File.Exists(oldfilePath))
                    {
                        System.IO.File.Delete(oldfilePath);
                    }
                    employee.ProfilePicture = fileName;
                }
                else
                {
                    employee.ProfilePicture = oldproduct.ProfilePicture;
                }

                employeeRepository.Edit(employee);
                employeeRepository.Commit();
                TempData["success"] = "Edited emplyee successfuly";
                return RedirectToAction("");
            }
            public IActionResult Delete(int id)
        {
            Employee employee = new Employee() { Id = id };
            employeeRepository.Delete(employee);
            employeeRepository.Commit();
            TempData["success"] = "Delete Employee Successfully";
            return RedirectToAction(""); 
        }
            }
}
