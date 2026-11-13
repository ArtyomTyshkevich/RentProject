using Microsoft.EntityFrameworkCore;
using User.BLL.Interfaces;
using UserService.DAL.Data;
using UserService.DAL.Entities;

namespace User.BLL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly UserDbContext _context;

        public ReviewRepository(UserDbContext context)
        {
            _context = context;
        }

        public Task<Review?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            _context.Reviews
                .Include(r => r.Pros)
                .Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.Id == id, ct);

        public async Task<IReadOnlyList<Review>> GetByReviewerIdAsync(Guid reviewerId, CancellationToken ct = default) =>
            await _context.Reviews
                .AsNoTracking()
                .Include(r => r.Pros)
                .Where(r => r.Reviewer.Id == reviewerId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(ct);

        public async Task<IReadOnlyList<Review>> GetAllAsync(CancellationToken ct = default) =>
            await _context.Reviews
                .AsNoTracking()
                .Include(r => r.Pros)
                .Include(r => r.Reviewer)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(ct);

        public async Task<Review> AddAsync(Review review, CancellationToken ct = default)
        {
            await _context.Reviews.AddAsync(review, ct);
            await _context.SaveChangesAsync(ct);
            return review;
        }

        public async Task UpdateAsync(Review review, CancellationToken ct = default)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Review review, CancellationToken ct = default)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync(ct);
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default) =>
            _context.Reviews.AnyAsync(r => r.Id == id, ct);
    }
}