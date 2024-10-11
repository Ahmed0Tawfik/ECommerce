using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Model.Interfaces;

namespace ECommerce.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(int id) => _context.Set<T>().Remove(Find(id));

        public IEnumerable<T> GetAll() => _context.Set<T>().ToList();

        public T Find(int id) => _context.Set<T>().FirstOrDefault(t => t.ID == id);

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
    }
}
