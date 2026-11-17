using Microsoft.EntityFrameworkCore;
using User.BLL.Interfaces;
using UserService.DAL.Data;
using UserService.DAL.Entities;

namespace UserService.DAL.Repositories
{
    public class ProsRepository : IProsRepository
    {
        private readonly UserDbContext _context;

        public ProsRepository(UserDbContext context)
        {
            _context = context;
        }

        public Task<Pros?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            _context.Pros
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<IReadOnlyList<Pros>> GetByIdsAsync(
            IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var idList = ids?.Distinct().ToList() ?? new List<Guid>();
            if (idList.Count == 0)
                return Array.Empty<Pros>();

            return await _context.Pros
                .Where(p => idList.Contains(p.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Pros>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _context.Pros
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

        public async Task<Pros> AddAsync(Pros pros, CancellationToken cancellationToken = default)
        {
            await _context.Pros.AddAsync(pros, cancellationToken);
            return pros;
        }

        public Task UpdateAsync(Pros pros, CancellationToken cancellationToken = default)
        {
            _context.Pros.Update(pros);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Pros pros, CancellationToken cancellationToken = default)
        {
            _context.Pros.Remove(pros);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
            _context.Pros.AnyAsync(p => p.Id == id, cancellationToken);
    }
}