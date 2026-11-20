using Chat.Applicaton.Interfaces;

namespace Chat.Data.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IChatRoomRepository ChatRoom { get; }

        void Dispose();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}