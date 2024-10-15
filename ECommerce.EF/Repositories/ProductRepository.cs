using ECommerce.Model.Interfaces;
using ECommerce.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EF.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context):base(context)
        {
        }

        public IEnumerable<Product> GetAllWithCategory()
        {
            return _context.Products.Include(x => x.Category).ToList();
        }

        public Product FindWithReview(int id)
        {
            return _context.Products.Include(x => x.Reviews).ThenInclude(x => x.User).FirstOrDefault(x => x.ID == id);
        }
        public IEnumerable<Product> GetFeaturedProducts()
        {
            //return 4 random products
            return _context.Products.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
