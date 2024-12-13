using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); // Get an entity by its ID
        Task<IEnumerable<T>> GetAllAsync(); // Get all entities
        Task<T> AddAsync(T entity); // Add a new entity
        Task<bool> UpdateAsync(T entity); // Update an entity
        Task<bool> DeleteAsync(int id); // Delete an entity by ID
    }
}
