using Chat.Domain.Enums;
using Chat.Domain.Models.Entites;

namespace Chat.Applicaton.Interfaces
{
    public interface IChatRoomRepository
    {
        Task<ChatRoom> AddAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default);
        void Delete(ChatRoom chatRoom);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatRoom>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatRoom>> GetByCreatorAsync(Guid creatorId, CancellationToken cancellationToken = default);
        Task<ChatRoom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatRoom>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ChatRoom>> GetByTypeAsync(ChatRoomType type, CancellationToken cancellationToken = default);
        void Update(ChatRoom chatRoom);
    }
}