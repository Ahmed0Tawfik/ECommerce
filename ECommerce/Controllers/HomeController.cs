using ECommerce.Model;
using ECommerce.Model.Interfaces;
using ECommerce.Model.Models;
using ECommerce.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        // Injecting the IUNITOFWORK interface

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel();
            model.FeaturedProducts = _unitOfWork.Products.GetFeaturedProducts();
            model.Reviews = _unitOfWork.Reviews.GetFeaturedReviews();


            //get 4 random products to display in the featured products section from the repository of products and 2 reviews from the repository of reviews 
            //not implemnted yet
            return View("Index", model);
        }

        public IActionResult About()
        {
            return View("About");
        }

        public IActionResult ContactUs()
        {
            return View("ContactUs");
        }
    }
}
