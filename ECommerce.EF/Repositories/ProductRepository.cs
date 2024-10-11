using ECommerce.Model.Interfaces;
using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EF.Repositories
{
    public class ProductRepository : BaseRepository<Product>,IProductRepository
    {
        public ProductRepository(ApplicationDbContext context):base(context)
        {
        }

        public IEnumerable<Product> GetFeaturedProducts()
        {
            //return 4 random products
            return _context.Products.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
