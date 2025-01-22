using GameService.Application.Interfaces.Repositories;
using GameService.Domain.Entities.Common;

namespace GameService.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> ReadRepository<T>() where T : BaseEntity;
        IWriteRepository<T> WriteRepository<T>() where T : BaseEntity;
        Task<int> SaveAsync();
    }
}
