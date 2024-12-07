using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Models;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class ReservationController : Controller
    {
        private readonly IReservationRepository reservationRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ITableRepository tableRepository;
        public ReservationController(IReservationRepository reservationRepository , ICustomerRepository customerRepository , ITableRepository tableRepository)
        {
            this.reservationRepository = reservationRepository;
            this.customerRepository = customerRepository;
            this.tableRepository = tableRepository;
        }
        public IActionResult Index(int page = 1, int pageSize = 5 , string search = null)
        {
             var cs = reservationRepository.GetAll([e => e.Customer, e => e.RestaurantTable]);
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

             var reservationsOnPage = cs.Skip((page - 1) * pageSize).Take(pageSize).ToList();

             ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            return View(reservationsOnPage); 
        }
        public IActionResult Create()
        {
            ViewBag.a = customerRepository.GetAll();
            ViewBag.b = tableRepository.GetAll();
            return View( );
        }
        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            reservationRepository.Add(reservation);
            reservationRepository.Commit();
            TempData["success"] = "Add Reservation successfully"; 
            return RedirectToAction("");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.a = customerRepository.GetAll();
            ViewBag.b = tableRepository.GetAll();

            var r = reservationRepository.GetOne([], e => e.Id == id).FirstOrDefault();
            return View(r);
        }
        [HttpPost]
        public IActionResult Edit(Reservation reservation)
        {
            reservationRepository.Edit(reservation);
            reservationRepository.Commit();
            TempData["success"] = "Edit Reservation successfully";
            return RedirectToAction("");

        }
        public IActionResult Delete(int id)
        {
            Reservation reservation = new Reservation() {Id=id};
            reservationRepository.Delete(reservation);
            reservationRepository.Commit();
            TempData["success"] = "Delete Reservation successfully";
            return RedirectToAction("");
        }
        public IActionResult ReservedTables()
        { //,e => e.reservations.Any()
            var reservedTables = tableRepository.GetAll([e=>e.reservations]).ToList();
  
            return View(reservedTables);
        }
    }
}
