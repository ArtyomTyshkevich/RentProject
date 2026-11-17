using UserService.BLL.DTOs.Identity;
using UserService.BLL.Interfaces;
using UserService.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserManagerRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<UserEntity> FindByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }
        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPasswordAsync(UserEntity user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<IdentityResult> UpdateUserAsync(UserEntity user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(UserEntity user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> AddToRoleAsync(UserEntity user)
        {
           return  await _userManager.AddToRoleAsync(user, RoleConsts.User);
        }

        public async Task<List<UserEntity>> UsersToListAsync(CancellationToken cancellationToken)
        {
          return await _userManager.Users.ToListAsync(cancellationToken);
        }
    }
}

