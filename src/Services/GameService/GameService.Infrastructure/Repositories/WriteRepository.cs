using GameService.Application.Interfaces.Repositories;
using GameService.Domain.Entities.Common;
using GameService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly GameServiceDbContext _context;
        private readonly DbSet<T> _table;

        public WriteRepository(GameServiceDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        public async Task AddAsync(T entity) => await _table.AddAsync(entity);
        public async Task AddRangeAsync(List<T> entities) => await _table.AddRangeAsync(entities);
        public void Update(T entity) => _table.Update(entity);
        public void Delete(T entity) => _table.Remove(entity);
    }
}
