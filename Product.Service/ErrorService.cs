using Product.Data.Infrastructure;
using Product.Data.Repositories;
using Product.Model.Models;

namespace Product.Service
{
    public interface IErrorService
    {
        Error Create(Error error);

        void Save();
    }

    public class ErrorService : IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUniOfWork _UnitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUniOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._UnitOfWork = unitOfWork;
        }

        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _UnitOfWork.Commit();
        }
    }
}