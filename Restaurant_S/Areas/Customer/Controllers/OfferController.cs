using Microsoft.AspNetCore.Mvc;
using  Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Customer")]

    public class OfferController : Controller
    {
        private readonly IMenuItemRepository menuItemRepository;
        private readonly ICategoryRepository categoryRepository;
        public OfferController(IMenuItemRepository menuItemRepository , ICategoryRepository categoryRepository)
        {
            this.menuItemRepository = menuItemRepository;
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index(string search = null, int page = 1, int pageSize = 5)
        {
             var cs = menuItemRepository.GetAll( [e => e.Category] , e => e.PriceBefore != 0);

             if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                cs = cs.Where(e => e.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

                if (!cs.Any())
                {
                    return RedirectToAction("NotFoundPage", "Home");
                }
            }
             int totalItems = cs.Count();   
            var paginatedCs = cs
                .Skip((page - 1) * pageSize)  
                .Take(pageSize)            
                .ToList();

             ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.SearchQuery = search;

            return View(paginatedCs);
        }
        public IActionResult Offer()
        {
          var offers =  menuItemRepository.GetAll([], e => e.PriceBefore != 0).ToList(); 
            return View(offers);
        }
        public IActionResult Details(int id)
        {
            var offer = menuItemRepository.GetOne([], e => e.Id == id).FirstOrDefault(); 

            return View(offer);
        }

    }
}
