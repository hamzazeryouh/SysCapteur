using Sys.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Interfaces
{
    public interface IBaseService<TEntity, TKey> where TEntity : class
    {
        Task<IResponse<TEntity>> GetByIdAsync(TKey id);
        Task<IResponse<List<TEntity>>> GetAllAsync();
        Task<IResponse<TEntity>> CreateAsync(TEntity entity);
        Task<IResponse<TEntity>> UpdateAsync(TEntity entity);
        Task<IResponse<bool>> DeleteAsync(TKey id);
    }
}
