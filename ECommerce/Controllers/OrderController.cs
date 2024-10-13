using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult CheckOut()
        {
            
            return View("Checkout");
        }

        public IActionResult OrderHistory()
        {
            return View("OrderHistory");
        }

        public IActionResult OrderDetails()
        {
            return View("OrderDetails");
        }

        public IActionResult OrderConfirmation()
        {
            return RedirectToAction("OrderDetails");
        }
    }
}
