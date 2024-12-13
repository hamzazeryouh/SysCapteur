using Sys.Application.Interfaces;
using Sys.Presistence.DataAccess;
using Sys.Presistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Services.BaseService
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<T> _repository;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.Repository<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var createdEntity = await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(); // Commit transaction
            return createdEntity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var isUpdated = await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(); // Commit transaction
            return isUpdated;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var isDeleted = await _repository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync(); // Commit transaction
            return isDeleted;
        }
    }
}
