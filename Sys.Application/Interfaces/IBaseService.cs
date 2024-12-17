using Sys.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    public interface IBaseService<TEntity, TKey, TDto> where TEntity : class 
    {
        Task<IResponse<TDto>> GetByIdAsync(TKey id);
        Task<IResponse<List<TDto>>> GetAllAsync();
        Task<IResponse<TDto>> CreateAsync(TDto entity);
        Task<IResponse<TDto>> UpdateAsync(TDto entity);
        Task<IResponse<bool>> DeleteAsync(TKey id);
    }
}
