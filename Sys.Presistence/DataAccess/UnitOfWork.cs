using Microsoft.EntityFrameworkCore;
using Sys.Presistence.DataContext;
using Sys.Presistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            var entityType = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(entityType))
            {
                _repositories[entityType] = new BaseRepository<TEntity, TKey>(_context);
            }

            return (IBaseRepository<TEntity, TKey>)_repositories[entityType];
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
