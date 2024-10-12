using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.ViewModels
{

    //View model for the home index page contains random 4 products to display in  the featured products section and 2 reviews from users
    public class HomeViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        public IEnumerable<Category> Categories { get; set; }

    }
}
