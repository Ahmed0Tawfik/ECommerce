using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        IEnumerable<Review> GetReviewsByProductId(int productId);

        IEnumerable<Review> GetFeaturedReviews();
    }
}
