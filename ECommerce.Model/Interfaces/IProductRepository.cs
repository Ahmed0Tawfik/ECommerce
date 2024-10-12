using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public IEnumerable<Product> GetProductsByCategory(int categoryId);
        
        // Get 4 random products as featured products 
        public IEnumerable<Product> GetFeaturedProducts();

        public IEnumerable<Product> GetAllWithCategory();
    }
}
