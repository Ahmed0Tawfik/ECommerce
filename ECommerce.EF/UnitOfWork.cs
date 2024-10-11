using ECommerce.EF.Repositories;
using ECommerce.Model;
using ECommerce.Model.Interfaces;
using ECommerce.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<Product> Products { get; private set; }
        public IBaseRepository<Category> Categories { get; private set; }
        public IBaseRepository<Review> Reviews { get; private set; }
        public IBaseRepository<Order> Orders { get; private set; }
        public IBaseRepository<OrderItem> OrderItems { get; private set; }
        public IBaseRepository<ShoppingCart> ShoppingCarts { get; private set; }
        public IBaseRepository<ShoppingCartItem> ShoppingCartItems { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new BaseRepository<Product>(_context);
            Categories = new BaseRepository<Category>(_context);
            Reviews = new BaseRepository<Review>(_context);
            Orders = new BaseRepository<Order>(_context);
            OrderItems = new BaseRepository<OrderItem>(_context);
            ShoppingCarts = new BaseRepository<ShoppingCart>(_context);
            ShoppingCartItems = new BaseRepository<ShoppingCartItem>(_context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
