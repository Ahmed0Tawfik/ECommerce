using ECommerce.Model.Interfaces;
using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Model
{
    public interface IUnitOfWork :IDisposable
    {
        IProductRepository Products { get; }
        IBaseRepository<Category> Categories { get; }
        IReviewRepository Reviews { get; }
        IBaseRepository<Order> Orders { get; }
        IBaseRepository<OrderItem> OrderItems { get; }
        IBaseRepository<ShoppingCart> ShoppingCarts { get; }
        IBaseRepository<ShoppingCartItem> ShoppingCartItems { get; }

        int Complete();
    }
}
