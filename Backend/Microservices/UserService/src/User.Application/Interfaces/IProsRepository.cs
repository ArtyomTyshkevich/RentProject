using UserService.DAL.Entities;

namespace User.BLL.Interfaces
{
    public interface IProsRepository
    {
        Task<Pros> AddAsync(Pros pros, CancellationToken cancellationToken = default);
        Task DeleteAsync(Pros pros, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Pros>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Pros?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Pros>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task UpdateAsync(Pros pros, CancellationToken cancellationToken = default);
    }
}