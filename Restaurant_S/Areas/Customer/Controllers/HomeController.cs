using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using Models;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMenuItemRepository menuItemRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly ISubmitReviewRepository submitReviewRepository;

        public HomeController(ILogger<HomeController> logger, IMenuItemRepository menuItemRepository, IOrderRepository orderRepository, IReservationRepository reservationRepository, ISubmitReviewRepository submitReviewRepository)
        {
            _logger = logger;
            this.menuItemRepository = menuItemRepository;
            this.orderRepository = orderRepository;
            this.reservationRepository = reservationRepository;
            this.submitReviewRepository = submitReviewRepository;

        }

        public IActionResult Index()
        {
            var menu = menuItemRepository.GetAll([e => e.orderMenuItems], e => e.PriceBefore == 0);
            return View(menu);
        }
        public IActionResult Details(int id)
        {
            ViewBag.orders = orderRepository.GetAll();
            var det = menuItemRepository.GetOne([e => e.Category], e => e.Id == id).FirstOrDefault();
            return View(det);
        }
        public IActionResult NotFoundPage()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult reviews()
        {
            var r = submitReviewRepository.GetAll().ToList();
            return View(r);
        }

        [HttpPost]
        public IActionResult SubmitReview(SubmitReview submitReview)
        {
            submitReviewRepository.Add(submitReview);
            submitReviewRepository.Commit();
            TempData["success"] = "Thank you for sharing your experience!";
            return RedirectToAction("Profile", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
