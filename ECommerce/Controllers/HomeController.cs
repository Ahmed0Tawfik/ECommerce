using ECommerce.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        // Injecting the IBaseRepository interface of the wanted entity => (IBaseRepository<T> _wantedEntityname Repo)
        public IActionResult Index()
        {
            return View();
        }
    }
}
