using Chat.Application.DTOs;
using Chat.Applicaton.Interfaces;
using Chat.Domain.Models.Entites;

namespace Chat.Application.Services.Implementation
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ChatRoomService(IChatRoomRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ChatRoomDto> CreateAsync(ChatRoomDto dto, CancellationToken cancellationToken = default)
        {
            var members = new List<Guid> { dto.Creator };
            if (dto.Members is not null)
            {
                foreach (var m in dto.Members)
                {
                    if (!members.Contains(m))
                        members.Add(m);
                }
            }

            var room = new ChatRoom
            {
                Id = Guid.NewGuid(),
                Creator = dto.Creator,
                Name = dto.Name,
                ChatRoomType = dto.ChatRoomType,
                Members = members
            };

            await _repository.AddAsync(room, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ToDto(room);
        }

        public async Task<ChatRoomDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var room = await _repository.GetByIdAsync(id, cancellationToken);
            return room is null ? null : ToDto(room);
        }

        public async Task<List<ChatRoomDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var rooms = await _repository.GetAllAsync(cancellationToken);
            return rooms.Select(ToDto).ToList();
        }

        public async Task<ChatRoomDto?> UpdateAsync(ChatRoomDto dto, CancellationToken cancellationToken = default)
        {
            var room = await _repository.GetByIdAsync(dto.Id, cancellationToken);
            if (room is null)
                return null;

            room.Name = dto.Name;
            room.ChatRoomType = dto.ChatRoomType;

            _repository.Update(room);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return ToDto(room);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var room = await _repository.GetByIdAsync(id, cancellationToken);
            if (room is null)
                return false;

            _repository.Delete(room);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        private static ChatRoomDto ToDto(ChatRoom room) => new()
        {
            Id = room.Id,
            Creator = room.Creator,
            Members = new List<Guid>(room.Members),
            Name = room.Name,
            ChatRoomType = room.ChatRoomType
        };
    }
}