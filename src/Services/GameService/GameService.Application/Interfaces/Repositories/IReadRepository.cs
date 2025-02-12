﻿using GameService.Application.Shared;
using GameService.Domain.Entities.Common;
using System.Linq.Expressions;

namespace GameService.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool enableTracking = false,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetWhere(Expression<Func<T, bool>>[]? predicates = null,
            bool enableTracking = false,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(string id, bool enableTracking = false, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>>[]? predicates = null,
            bool enableTracking = false,
            params Expression<Func<T, object>>[] includes);

        Task<PaginatedResult<T>> GetWithPaginationAsync(
            int page = 1,
            int pageSize = 10,
            Expression<Func<T, bool>>[]? predicates = null,
            Expression<Func<T, object>>? orderBy = null,
            bool isDescending = false,
            bool enableTracking = false,
            params Expression<Func<T, object>>[] includes);
    }
}
