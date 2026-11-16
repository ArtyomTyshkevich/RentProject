using Library.Application.Interfaces;
using Library.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using User.BLL.Interfaces;
using User.BLL.Repositories;
using UserService.BLL.Interfaces;
using UserService.DAL.Data;
using UserService.DAL.Entities;
using UserService.DAL.Repositories;

namespace UserService.BLL.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _userDbContext;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public UnitOfWork(UserDbContext libraryDbContext, IWebHostEnvironment env, IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _env = env;
            _userDbContext = libraryDbContext;
            _userManager = userManager;
            _configuration = configuration;
            Users = new UserRepository(_userDbContext);
            UserManagers = new UserManagerRepository(_userManager);
            ProsRepository = new ProsRepository(_userDbContext);
            ReviewRepository = new ReviewRepository(_userDbContext);
        }

        public IUserRepository Users { get; private set; }
        public IUserManagerRepository UserManagers { get; private set; }
        public IProsRepository ProsRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _userDbContext.SaveChangesAsync(cancellationToken);
        public void Dispose() => _userDbContext.Dispose();
    }
}