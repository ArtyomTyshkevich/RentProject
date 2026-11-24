using Library.Data.Repositories;
using User.BLL.Interfaces;
using UserService.BLL.Interfaces;

namespace Library.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IUserManagerRepository UserManagers { get; }
        IReviewRepository ReviewRepository { get; }
        IProsRepository ProsRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}