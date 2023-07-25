using BusinessLogic.EFUnitOfWork;
using BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly UnitOfWork _unitOfWork;
        protected readonly Repository<T> _repository;

        public BaseService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.GetRepository<T>();

        }
    }
}
