using GameService.Application.Interfaces.Repositories;
using GameService.Application.Interfaces.UnitOfWorks;
using GameService.Domain.Entities.Common;
using GameService.Infrastructure.Context;
using GameService.Infrastructure.Repositories;

namespace GameService.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        GameServiceDbContext _context;
        public UnitOfWork(GameServiceDbContext context)
        {
            _context = context;
        }
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();

        public Task<int> SaveAsync() => _context.SaveChangesAsync();

        public IReadRepository<T> ReadRepository<T>() where T : BaseEntity
            => new ReadRepository<T>(_context);

        public IWriteRepository<T> WriteRepository<T>() where T : BaseEntity
            => new WriteRepository<T>(_context);
    }
}
