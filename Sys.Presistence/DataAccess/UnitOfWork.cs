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

        // Dictionary to store repositories, avoiding repeated creation of repositories
        private readonly Dictionary<string, object> _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        public IBaseRepository<T> Repository<T>() where T : class
        {
            // Check if repository already exists, otherwise create it
            var key = typeof(T).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repository = new BaseRepository<T>(_context);
                _repositories.Add(key, repository);
            }

            return (IBaseRepository<T>)_repositories[key];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(); // Commit all changes to the database
        }
    }
}
