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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var updatedEntity =  _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entityToDelete = await _context.Set<T>().FindAsync(id);
            var deleted = _context.Set<T>().Remove(entityToDelete);
            await _context.SaveChangesAsync();

        }
        public async Task DeleteAsync(T entity)
        {
            var deletedEntity = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }
}
