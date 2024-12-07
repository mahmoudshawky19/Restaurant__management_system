using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository; 
 
        public OrderController(IOrderRepository orderRepository , ICustomerRepository customerRepository  )
        {
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository; 
         }
        public IActionResult Index(string search = null, int page = 1)
        {
            var pageSize = 5;  
            var cs = orderRepository.GetAll([e => e.Customer]); 

             if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                cs = cs.Where(e => e.Customer.Name.Contains(search));  

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
            ViewBag.StatusList = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(s => new SelectListItem{Value = s.ToString(),Text = s.ToString()}).ToList();
            ViewBag.a = customerRepository.GetAll();
            var c =  orderRepository.GetAll().ToList();
            return View(c );
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {  
            orderRepository.Add(order);
            orderRepository.Commit();
            TempData["success"] = "Add Order successfully";
            return RedirectToAction("");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.StatusList = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().Select(s => new SelectListItem { Value = s.ToString(), Text = s.ToString() }).ToList();
            ViewBag.a = customerRepository.GetAll();

            var c = orderRepository.GetOne([],e => e.Id == id).FirstOrDefault();
             return View(c);
        }
        [HttpPost]
        public IActionResult Edit(Order  order)
        {
            orderRepository.Edit(order);
            orderRepository.Commit();
            TempData["success"] = "Edit Order successfully";
            return RedirectToAction("");

        }
        public IActionResult Delete(int id)
        {
            Order order = new Order() { Id = id };
            orderRepository.Delete(order);
            orderRepository.Commit();
            TempData["success"] = "Delete Order successfully";
            return RedirectToAction("");
        }
    }
}
