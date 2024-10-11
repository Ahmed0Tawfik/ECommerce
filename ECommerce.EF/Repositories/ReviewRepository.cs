using ECommerce.Model.Interfaces;
using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EF.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Review> GetFeaturedReviews()
        {
            return _context.Reviews.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
        }

        public IEnumerable<Review> GetReviewsByProductId(int productId)
        {
            throw new NotImplementedException();
        }
    }
   
}
