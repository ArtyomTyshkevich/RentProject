using UserService.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace UserService.BLL.Interfaces
{
    public interface IUserManagerRepository
    {
        Task<IdentityResult> CreateUserAsync(UserEntity user, string password);
        Task<IdentityResult> DeleteUserAsync(UserEntity user);
        Task<UserEntity> FindByNameAsync(string name);
        Task<UserEntity> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(UserEntity user, string password);
        Task<IdentityResult> UpdateUserAsync(UserEntity user);
        Task<IdentityResult> AddToRoleAsync(UserEntity user);
        Task<List<UserEntity>> UsersToListAsync(CancellationToken cancellationToken);
    }
}