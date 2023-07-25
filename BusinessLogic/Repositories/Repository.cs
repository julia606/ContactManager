using DataAccess.EF;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            var addedEntity = _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            var updatedEntity = _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = this.Get(id);
            this.Delete(entity);

        }
        public void Delete(T entity)
        {
            var deletedEntity = _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

    }
}
