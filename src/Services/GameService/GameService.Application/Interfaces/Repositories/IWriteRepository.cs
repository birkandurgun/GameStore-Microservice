using GameService.Domain.Entities.Common;

namespace GameService.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }
}
