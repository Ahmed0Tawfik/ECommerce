using ECommerce.Model;
using ECommerce.Model.Models;
using ECommerce.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int page = 1, int pageSize = 6)
        {
            var Products = _unitOfWork.Products.GetAllWithCategory()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalProducts = _unitOfWork.Products.GetAll().Count();

            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var model = new ProductListViewModel
            {
                Products = Products,
                CurrentPage = page,
                TotalPages = totalPages
            };
                
            return View("Index", model);
        }

        public IActionResult Details(int id)
        {
            var product = _unitOfWork.Products.FindWithReview(id);
            
            return View("Details", product);
        }

        public IActionResult ByCategory(int id, int page = 1, int pageSize = 6)
        {
            var Products = _unitOfWork.Products.GetProductsByCategory(id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalProducts = _unitOfWork.Products.GetProductsByCategory(id).Count();

            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            var model = new ProductListViewModel
            {
                Products = Products,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View("Index", model);
        }

        [Authorize]
        public IActionResult AddReview(int id, string content)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            var review = new Review
            {
                UserId = userId,
                ProductId = id,
                Content = content
            };

            _unitOfWork.Reviews.Add(review);
            _unitOfWork.Complete();
            TempData["Login"] = "Review added successfully";
            return RedirectToAction("Details", new { id = id });
        }
    }
}
