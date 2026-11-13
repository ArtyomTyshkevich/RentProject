using UserService.BLL.Interfaces;
using UserService.DAL.Data;
using UserService.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _authDbContext;

        public UserRepository(UserDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public async Task<List<UserEntity>> GetAsync(CancellationToken cancellationToken)
        {
            return await _authDbContext.Users
                                            .Include(user => user.UserName)
                                            .ToListAsync(cancellationToken);
        }

        public async Task<UserEntity?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _authDbContext.Users
                                 .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
        }

        public async Task<List<IdentityRole<Guid>>> GetRolesAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var roles = await _authDbContext.Roles
                .Where(role => _authDbContext.UserRoles
                    .Where(contextUser => contextUser.UserId == user.Id)
                    .Select(contextUser => contextUser.RoleId)
                    .Contains(role.Id))
                .ToListAsync(cancellationToken);

            return roles;
        }

        public async Task<UserEntity> GetByEmailAsync(string Email, CancellationToken cancellationToken)
        {
            return await _authDbContext.Users
                .FirstAsync(user => user.Email == Email, cancellationToken);
        }
        public async Task UpdateAsync(UserEntity user, CancellationToken cancellationToken = default)
        {
            _authDbContext.Users
                .Update(user);
        }

        public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _authDbContext.Users
                .FindAsync(userId, cancellationToken);

            if (user != null)
            {
                _authDbContext.Users
                    .Remove(user);
            }
        }
    }
}
