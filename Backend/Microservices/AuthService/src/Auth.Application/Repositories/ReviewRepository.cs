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
            // Прикрепляем Pros к контексту, чтобы EF не пытался их создавать (INSERT)
            foreach (var pros in review.Pros)
            {
                var entry = _context.Entry(pros);
                if (entry.State == EntityState.Detached)
                {
                    _context.Pros.Attach(pros);
                }
            }

            await _context.Reviews.AddAsync(review, ct);
            return review;
        }

        public Task UpdateAsync(Review review, CancellationToken ct = default)
        {
            // Прикрепляем Review
            _context.Reviews.Attach(review);

            // Помечаем Review как Modified, чтобы EF обновил его поля
            _context.Entry(review).State = EntityState.Modified;

            // Для каждого Pros: помечаем как Unchanged, чтобы EF не пытался их вставить/обновить
            foreach (var pros in review.Pros)
            {
                var entry = _context.Entry(pros);
                if (entry.State == EntityState.Detached)
                {
                    _context.Pros.Attach(pros);
                }
                entry.State = EntityState.Unchanged;
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Review review, CancellationToken ct = default)
        {
            _context.Reviews.Remove(review);
            return Task.CompletedTask; // Убрали async, так как нет await
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default) =>
            _context.Reviews.AnyAsync(r => r.Id == id, ct);
    }
}