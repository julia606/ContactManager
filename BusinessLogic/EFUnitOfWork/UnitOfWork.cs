using BusinessLogic.Repositories;
using DataAccess.EF;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.EFUnitOfWork
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork()
        {
            _context = new AppDbContext();
        }
        public Repository<T>? GetRepository<T>() where T : class
        {
            return new Repository<T>(_context);
        }
    }
}
