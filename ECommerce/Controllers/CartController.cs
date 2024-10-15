using ECommerce.Model;
using ECommerce.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;   
            _userManager = userManager;    
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            var user = await _userManager.Users
                                .Include(u => u.ShoppingCart)
                                .ThenInclude(c => c.ShoppingCartItems)
                                .ThenInclude(ci => ci.Product)
                                .FirstOrDefaultAsync(u => u.Id == userId); // Get logged-in user ID

            var cart = user.ShoppingCart;

            if (cart == null)
            {
                cart = new ShoppingCart 
                { 
                    UserId = userId,
                    ShoppingCartItems = new List<ShoppingCartItem>(),
                };
                _unitOfWork.ShoppingCarts.Add(cart);
                _unitOfWork.Complete();
            }

            return View("Index",cart);
        }

        public async Task<IActionResult> RemoveFromCart(int Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID
            var cart = await _unitOfWork.ShoppingCarts.GetAllQueryable()
                .Include(c => c.ShoppingCartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);


            var cartItem = cart.ShoppingCartItems.FirstOrDefault(ci => ci.ProductId == Id);

            if (cartItem != null)
            {
                cartItem.Product.Stock += cartItem.Quantity;
                cart.ShoppingCartItems.Remove(cartItem);
                _unitOfWork.Complete();
            }
            TempData["Logout"] = "Product removed from cart";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            var user = await _userManager.Users
                                .Include(u => u.ShoppingCart)
                                .ThenInclude(c => c.ShoppingCartItems)
                                .ThenInclude(ci => ci.Product)
                                .FirstOrDefaultAsync(u => u.Id == userId); // Get logged-in user ID
            var product = _unitOfWork.Products.Find(id);
            product.Stock--;

            // Get or create the user's shopping cart
            var shoppingCart = user.ShoppingCart ?? new ShoppingCart { UserId = user.Id, ShoppingCartItems = new List<ShoppingCartItem>() };

            // Check if the product is already in the cart
            var existingCartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(item => item.ProductId == id);
            if (existingCartItem != null)
            {
                // If the item is already in the cart, update the quantity
                existingCartItem.Quantity += quantity;
            }
            else
            {
                // Otherwise, add the new item to the cart
                var cartItem = new ShoppingCartItem
                {
                    ProductId = id,
                    Quantity = quantity,
                    ShoppingCart = shoppingCart
                };
                shoppingCart.ShoppingCartItems.Add(cartItem);
            }

            // If it's a new cart, add it to the context
            if (user.ShoppingCart == null)
            {
                _unitOfWork.ShoppingCarts.Add(shoppingCart);
                user.ShoppingCart = shoppingCart;
            }

            _unitOfWork.Complete();

            // Redirect or return success
            TempData["Login"] = "Product added to cart";
            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID
            var cart = await _unitOfWork.ShoppingCarts.GetAllQueryable()
                .Include(c => c.ShoppingCartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

           foreach(var item in cart.ShoppingCartItems)
            {
                item.Product.Stock += item.Quantity;
            }
           cart.ShoppingCartItems.Clear();
            _unitOfWork.Complete();
            TempData["Logout"] = "Cart cleared";
            return RedirectToAction("Index","Home");
        }
    }
}
