using UserService.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace UserService.BLL.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserEntity>> GetAsync(CancellationToken cancellationToken);
        Task<UserEntity?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<List<IdentityRole<Guid>>> GetRolesAsync(UserEntity user, CancellationToken cancellationToken);
        Task<UserEntity> GetByEmailAsync(string mail, CancellationToken cancellationToken);
        Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
        Task UpdateAsync(UserEntity user, CancellationToken cancellationToken = default);
    }
}