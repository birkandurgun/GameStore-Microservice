using GameService.Application.Interfaces.Repositories;
using GameService.Application.Shared;
using GameService.Domain.Entities.Common;
using GameService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameService.Infrastructure.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly GameServiceDbContext _context;
        private readonly DbSet<T> _table;

        public ReadRepository(GameServiceDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public IQueryable<T> GetAll(bool enableTracking = false,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            foreach (var include in includes) queryable = queryable.Include(include);
            if (orderBy != null)
                queryable = isDescending
                    ? queryable.OrderByDescending(orderBy)
                    : queryable.OrderBy(orderBy);
            return queryable;
        }

        public async Task<T> GetByIdAsync(string id, bool enableTracking = false, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            foreach (var include in includes) queryable = queryable.Include(include);
            return await queryable.FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>>[]? predicates = null,
            bool enableTracking = false,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            foreach (var include in includes) queryable = queryable.Include(include);
            if (predicates != null && predicates.Any())
                foreach (var predicate in predicates)
                    queryable = queryable.Where(predicate);
            if (orderBy != null)
                queryable = isDescending
                    ? queryable.OrderByDescending(orderBy)
                    : queryable.OrderBy(orderBy);
            return queryable;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>>[]? predicates = null,
            bool enableTracking = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            foreach (var include in includes) queryable = queryable.Include(include);
            if (predicates != null && predicates.Any())
                foreach (var predicate in predicates)
                    queryable = queryable.Where(predicate);

            return await queryable.FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<T>> GetWithPaginationAsync(
            int page = 1,
            int pageSize = 10,
            Expression<Func<T, bool>>[]? predicates = null,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            bool enableTracking = false,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            foreach (var include in includes) queryable = queryable.Include(include);
            if (predicates != null && predicates.Any())
                foreach (var predicate in predicates)
                    queryable = queryable.Where(predicate);
            if (orderBy != null)
                queryable = isDescending
                    ? queryable.OrderByDescending(orderBy)
                    : queryable.OrderBy(orderBy);

            int totalCount = await queryable.CountAsync();
            var data = await queryable
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<T>
            {
                Data = data,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

        }
    }
}
