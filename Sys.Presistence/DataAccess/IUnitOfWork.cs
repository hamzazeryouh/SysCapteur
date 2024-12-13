using Sys.Presistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.DataAccess
{
    public interface IUnitOfWork
    {
        IBaseRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
