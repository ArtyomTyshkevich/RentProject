namespace Chat.Applicaton.Interfaces
{
    public interface IUnitOfWork
    {
        IChatRoomRepository ChatRoom { get; }

        void Dispose();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}