using Sys.Presistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;
        Task CompleteAsync();
    }
}
