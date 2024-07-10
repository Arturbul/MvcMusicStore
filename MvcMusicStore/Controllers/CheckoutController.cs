using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly MusicStoreEntities _storeDB;
        private readonly ILogger<CheckoutController> _logger;
        private const string PromoCode = "FREE";

        public CheckoutController(MusicStoreEntities storeDB, ILogger<CheckoutController> logger)
        {
            _storeDB = storeDB;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddressAndPayment(Order order, string promoCode)
        {
            if (ModelState.IsValid)
            {
                if (!string.Equals(promoCode, PromoCode, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("", "Invalid Promo Code");
                    return View(order);
                }

                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;

                _storeDB.Orders.Add(order);
                var result = _storeDB.SaveChanges();

                if (result > 0)
                {
                    _logger.LogInformation($"Order {order.OrderId} successfully created.");
                }
                else
                {
                    _logger.LogError("Order was not saved to the database.");
                }

                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete", new { id = order.OrderId });
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult Complete(int id)
        {
            bool isValid = _storeDB.Orders.Any(o => o.OrderId == id && o.Username == User.Identity.Name);

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
