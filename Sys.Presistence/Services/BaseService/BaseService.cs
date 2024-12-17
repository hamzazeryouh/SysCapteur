using AutoMapper;
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
    public class BaseService<TEntity, TKey, TDto> : IBaseService<TEntity, TKey, TDto>
        where TEntity : class
        where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<TEntity, TKey> _repository;

        public BaseService(IUnitOfWork unitOfWork, IBaseRepository<TEntity, TKey> repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        private TDto MapToDto(TEntity entity)
        {
            
            return _mapper.Map<TDto>(entity);
        }

        public async Task<IResponse<TDto>> GetByIdAsync(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                var error = new CustomException(errorCode: 1001, message: $"{typeof(TEntity).Name} not found.");
                return new Response<TDto>(error, statusCode: 404);
            }

            var dto = MapToDto(entity);
            return new Response<TDto>(dto);
        }

        public async Task<IResponse<List<TDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = entities.Select(MapToDto).ToList();
            return new Response<List<TDto>>(dtos);
        }

        public async Task<IResponse<TDto>> CreateAsync(TDto model)
        {
            var entity=  _mapper.Map<TEntity>(model);
            var isCreated = await _repository.AddAsync(entity);
            if (!isCreated)
            {
                var error = new CustomException(errorCode: 1002, message: "Error creating entity.");
                return new Response<TDto>(error, statusCode: 400);
            }

            await _unitOfWork.CompleteAsync();
            var dto = MapToDto(entity);
            return new Response<TDto>(dto, statusCode: 201);
        }

        public async Task<IResponse<TDto>> UpdateAsync(TDto model)
        {
            var entity = _mapper.Map<TEntity>(model);
            var isUpdated = await _repository.UpdateAsync(entity);
            if (!isUpdated)
            {
                var error = new CustomException(errorCode: 1003, message: "Error updating entity.");
                return new Response<TDto>(error, statusCode: 400);
            }

            await _unitOfWork.CompleteAsync();
            var dto = MapToDto(entity);
            return new Response<TDto>(dto);
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
