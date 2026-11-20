using Chat.Application.DTOs;

namespace Chat.Applicaton.Interfaces
{
    public interface IChatRoomService
    {
        Task<ChatRoomDto> CreateAsync(ChatRoomDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<ChatRoomDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ChatRoomDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ChatRoomDto?> UpdateAsync(ChatRoomDto dto, CancellationToken cancellationToken = default);
    }
}