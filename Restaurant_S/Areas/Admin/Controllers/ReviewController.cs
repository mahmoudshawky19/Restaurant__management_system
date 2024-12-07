using Microsoft.AspNetCore.Mvc;
using  Models;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;

namespace Restaurant_S.Controllers
{
    [Area("Admin")]

    public class ReviewController : Controller
    {
        private readonly ISubmitReviewRepository submitReviewRepository;
        public ReviewController(ISubmitReviewRepository submitReviewRepository)
        {
            this.submitReviewRepository = submitReviewRepository;
        }
        public IActionResult Index(string search = null, int page = 1, int pageSize = 5)
        {
            var cs = submitReviewRepository.GetAll();

             if (!string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                cs = cs.Where(e => e.Name.Contains(search));

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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SubmitReview submitReview)
        {
            submitReviewRepository.Add(submitReview);
            submitReviewRepository.Commit();
            TempData["success"] = "Add Review successfully";

            return RedirectToAction("");
        }
        public IActionResult Edit(int id)
        {
            var cat = submitReviewRepository.GetOne([]).FirstOrDefault(e => e.Id == id);
            return View(cat);
        }
        [HttpPost]

        public IActionResult Edit(SubmitReview submitReview)
        {
            submitReviewRepository.Edit(submitReview);
            submitReviewRepository.Commit();
            TempData["success"] = "Edit Review successfully";
            return RedirectToAction("");
        }
        public IActionResult Delete(int id)
        {
            SubmitReview cs = new SubmitReview() { Id = id };
            submitReviewRepository.Delete(cs);
            submitReviewRepository.Commit();
            TempData["success"] = "Delete Review successfully";
            return RedirectToAction("");

        }


    }
}
