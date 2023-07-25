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
        private AppDbContext context;
        public UnitOfWork()
        {
            context = new AppDbContext();
        }
        public Repository<T>? GetRepository<T>() where T : class
        {
            return new Repository<T>(context);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
