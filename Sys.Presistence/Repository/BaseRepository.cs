using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sys.Application.Helpers;
using Sys.Presistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.Repository
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<TEntity>> ApplyFiltersAsync(FilterModel<TEntity> filterModel)
        {
            // Start with the base query
            var query = _context.Set<TEntity>().AsQueryable();

            // Apply filters dynamically
            foreach (var filter in filterModel.Filters)
            {
                query = ApplyFilter(query, filter.Key, filter.Value);
            }

            // Apply sorting
            query = ApplySorting(query, filterModel.OrderBy, filterModel.OrderDirection);

            // Apply pagination
            query = query.Skip((filterModel.PageNumber - 1) * filterModel.PageSize)
                         .Take(filterModel.PageSize);

            return query;
        }

        // Apply the filter condition based on the property name and value
        private IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, string propertyName, object value)
        {
            if (string.IsNullOrEmpty(propertyName) || value == null)
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var propertyExpression = Expression.Property(parameter, propertyName);
            var constantExpression = Expression.Constant(value);

            var equalsExpression = Expression.Equal(propertyExpression, constantExpression);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equalsExpression, parameter);

            return query.Where(lambda);
        }

        // Apply sorting to the query
        private IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string orderBy, string orderDirection)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return query; // No sorting if OrderBy is not provided
            }

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var propertyExpression = Expression.Property(parameter, orderBy);
            var lambda = Expression.Lambda(propertyExpression, parameter);

            var methodName = orderDirection.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
            var genericMethod = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TEntity), propertyExpression.Type);

            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { query, lambda });
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
