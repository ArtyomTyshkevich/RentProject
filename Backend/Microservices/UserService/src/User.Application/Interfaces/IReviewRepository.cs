using UserService.DAL.Entities;

namespace User.BLL.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> AddAsync(Review review, CancellationToken ct = default);
        Task DeleteAsync(Review review, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Review>> GetAllAsync(CancellationToken ct = default);
        Task<Review?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Review>> GetByReviewerIdAsync(Guid reviewerId, CancellationToken ct = default);
        Task UpdateAsync(Review review, CancellationToken ct = default);
    }
}