using Sys.Application.Helpers;
using Sys.Application.Interfaces;
using Sys.Presistence.DataAccess;
using Sys.Presistence.Repository;
using SysCapteur.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Services.BaseService
{
    public class BaseService<TEntity, TKey> : IBaseService<TEntity, TKey> where TEntity : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<TEntity, TKey> _repository;

        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity, TKey> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<IResponse<TEntity>> GetByIdAsync(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                var error = new CustomException(errorCode: 1001, message: $"{typeof(TEntity).Name} not found.");
                return new Response<TEntity>(error, statusCode: 404);
            }

            return new Response<TEntity>(entity);
        }

        public async Task<IResponse<List<TEntity>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return new Response<List<TEntity>>(entities.ToList());
        }

        public async Task<IResponse<TEntity>> CreateAsync(TEntity entity)
        {
            var isCreated = await _repository.AddAsync(entity);
            if (!isCreated)
            {
                var error = new CustomException(errorCode: 1002, message: "Error creating entity.");
                return new Response<TEntity>(error, statusCode: 400);
            }

            await _unitOfWork.CompleteAsync();
            return new Response<TEntity>(entity, statusCode: 201);
        }

        public async Task<IResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            var isUpdated = await _repository.UpdateAsync(entity);
            if (!isUpdated)
            {
                var error = new CustomException(errorCode: 1003, message: "Error updating entity.");
                return new Response<TEntity>(error, statusCode: 400);
            }

            await _unitOfWork.CompleteAsync();
            return new Response<TEntity>(entity);
        }

        public async Task<IResponse<bool>> DeleteAsync(TKey id)
        {
            var isDeleted = await _repository.DeleteAsync(id);
            if (!isDeleted)
            {
                var error = new CustomException(errorCode: 1004, message: "Error deleting entity.");
                return new Response<bool>(error, statusCode: 400);
            }

            await _unitOfWork.CompleteAsync();
            return new Response<bool>(true);
        }
    }

}
