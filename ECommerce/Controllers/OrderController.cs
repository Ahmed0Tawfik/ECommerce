using ECommerce.Model.Models;
using ECommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ECommerce.Model.ViewModels;
using Microsoft.Identity.Client;

namespace ECommerce.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult CheckOut()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            var user =  _userManager.Users
                                 .Include(u => u.ShoppingCart)
                                 .ThenInclude(sc => sc.ShoppingCartItems)
                                 .ThenInclude(sci => sci.Product)
                                 .FirstOrDefault(u => u.Id == userId); // Get logged-in user ID
            CheckOutViewModel checkOutViewModel = new CheckOutViewModel();
            checkOutViewModel.User = user;
            

            return View("Checkout", checkOutViewModel);
        }

        public IActionResult OrderHistory()
        {
            return View("OrderHistory");
        }

        public IActionResult OrderDetails()
        {
            return View("OrderDetails");
        }

        public IActionResult OrderConfirmation(CheckOutViewModel checkOutViewModel)
        {
            if(ModelState.IsValid == false)
            {
                return View("Checkout", checkOutViewModel);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            var user = _userManager.Users
                                 .Include(u => u.ShoppingCart)
                                 .ThenInclude(sc => sc.ShoppingCartItems)
                                 .ThenInclude(sci => sci.Product)
                                 .Include(u => u.Orders)
                                 .FirstOrDefault(u => u.Id == userId); // Get logged-in user ID


            var orderitems = new List<OrderItem>();
           

            foreach(var item in user.ShoppingCart.ShoppingCartItems)
            {
                orderitems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderItems = orderitems,
                TotalAmount = user.ShoppingCart.TotalAmount + 50.0m,
                Status = "Pending",
                OrderFullName = checkOutViewModel.OrderFullName,
                OrderPhoneNumber = checkOutViewModel.OrderPhoneNumber,
                PaymentMethod = checkOutViewModel.PaymentMethod,
                OptionalEmail = checkOutViewModel.OrderOptionalEmail,
                Address = $"{checkOutViewModel.State} - {checkOutViewModel.Address}",
                UserId = user.Id
            };

            
            if(checkOutViewModel.PaymentMethod == PaymentMethod.CashOnDelivery)
            {
                _unitOfWork.Orders.Add(order);
                _unitOfWork.ShoppingCarts.Delete(user.ShoppingCart.ID);
                _unitOfWork.Complete();
                TempData["Login"] = "Order Has Been Placed";

                return View("OrderConfirmation", order);
            }
            else
            {
                OrderPaymentViewModel orderPaymentViewModel = new OrderPaymentViewModel();
                orderPaymentViewModel.Order = order;
                return View("PaymentGate", orderPaymentViewModel);
            }

            
            
        }
        
        public IActionResult PaymentConfirmation(OrderPaymentViewModel orderPaymentViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            var user = _userManager.Users
                                 .Include(u => u.ShoppingCart)
                                 .FirstOrDefault(u => u.Id == userId); // Get logged-in user ID

            //Payment Confirmation Logic Not implemented yet
            //if(//somelogic here)
            //{
            _unitOfWork.Orders.Add(orderPaymentViewModel.Order);
            _unitOfWork.ShoppingCarts.Delete(user.ShoppingCart.ID);
            _unitOfWork.Complete();
            TempData["Login"] = "Order Has Been Placed";
            //}

            return View("OrderConfirmation", orderPaymentViewModel.Order);
        }
    }
}
