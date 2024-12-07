using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Models;
using DataAccess.Repository.IRepository;
 
namespace Restaurant_S.Controllers
{
    [Area("Customer")]

    public class CartController : Controller
    {
        private readonly ICartRepository cartRepositery;
        private readonly UserManager<IdentityUser> userManager;
        public CartController(ICartRepository cartRepositery, UserManager<IdentityUser> userManager)
        {
            this.cartRepositery = cartRepositery;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var c = cartRepositery.GetAll([e => e.MenuItem], e => e.ApplicationUserId == ApplicationUserId).ToList();
            ViewBag.Total = c.Sum(e => e.MenuItem.PriceAfter * e.count);
            return View(c);
        }
        public IActionResult AddToCart(int MenuItemId, int count)
        {
            var us = userManager.GetUserId(User);
            if (us == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            var existingCartItem = cartRepositery.GetAll(null, e => e.MenuItemId == MenuItemId && e.ApplicationUserId == userManager.GetUserId(User)).FirstOrDefault();
            if (existingCartItem != null)
            {
                existingCartItem.count += count;  
                cartRepositery.Edit(existingCartItem);
            }
            else
            {
                Cart cart = new Cart()
                {
                    count = count,
                    MenuItemId = MenuItemId,
                    ApplicationUserId = userManager.GetUserId(User)
                };
                cartRepositery.Add(cart);
            }
            cartRepositery.Commit();
            return RedirectToAction("Index");
        }
        public IActionResult Increment(int menuItemid)
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var menuitem = cartRepositery.GetOne(expression: e => e.ApplicationUserId == ApplicationUserId && e.MenuItemId == menuItemid).FirstOrDefault();

            if (menuitem != null)
            {
                menuitem.count++;
                cartRepositery.Commit();

                return RedirectToAction("Index");
            }

            return RedirectToAction("NotFoundPage", "Home", new { area = "Customer" });
        }
        public IActionResult Decrement(int menuItemid)
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var menuitem = cartRepositery.GetOne(expression: e => e.ApplicationUserId == ApplicationUserId && e.MenuItemId == menuItemid).FirstOrDefault();

            if (menuitem != null)
            {
                menuitem.count--;
                if (menuitem.count > 0)

                    cartRepositery.Commit();

                else
                    menuitem.count = 1;

                return RedirectToAction("Index");
            }

            return RedirectToAction("NotFoundPage", "Home", new { area = "Customer" });
        }
        public IActionResult Delete(int menuItemid)
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var menuitem = cartRepositery.GetOne(expression: e => e.ApplicationUserId == ApplicationUserId && e.MenuItemId == menuItemid).FirstOrDefault();

            if (menuitem != null)
            {
                cartRepositery.Delete(menuitem);
                cartRepositery.Commit();

                return RedirectToAction("Index");
            }

            return RedirectToAction("NotFoundPage", "Home", new { area = "Customer" });
        }
        public IActionResult Pay()
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var cartProduct = cartRepositery.GetAll([e => e.MenuItem], e => e.ApplicationUserId == ApplicationUserId).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            foreach (var item in cartProduct)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.MenuItem.Name,
                        },
                        UnitAmount = (long)item.MenuItem.PriceAfter * 100,
                    },
                    Quantity = item.count,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }

    }
}
