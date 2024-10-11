using ECommerce.Model.Interfaces;
using ECommerce.Model.Models;
using ECommerce.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        // Injecting the IBaseRepository interface of the wanted entity => (IBaseRepository<T> _wantedEntityname Repo)
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            //get 4 random products to display in the featured products section from the repository of products and 2 reviews from the repository of reviews 
            //not implemnted yet
            return View("Index", model);
        }
    }
}
