using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly MusicStoreEntities _storeDB;
        private const string PromoCode = "FREE";

        public CheckoutController(MusicStoreEntities storeDB)
        {
            _storeDB = storeDB;
        }

        // GET: /Checkout/AddressAndPayment
        [HttpGet]
        public IActionResult AddressAndPayment()
        {
            return View();
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public IActionResult AddressAndPayment(Order order, string promoCode)
        {
            if (ModelState.IsValid)
            {
                if (string.Equals(promoCode, PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    ModelState.AddModelError("", "Invalid Promo Code");
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    // Save Order
                    _storeDB.Orders.Add(order);
                    _storeDB.SaveChanges();

                    // Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            // If we got this far, something failed, redisplay form
            return View(order);
        }

        // GET: /Checkout/Complete
        [HttpGet]
        public IActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = _storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
