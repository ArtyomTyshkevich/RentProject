using Chat.Applicaton.Interfaces;
using Chat.Data.Context;
using Chat.Domain.Enums;
using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly ChatDbContext _context;

        public ChatRoomRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<ChatRoom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ChatRooms
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ChatRoom>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ChatRooms
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ChatRoom>> GetByMemberAsync(Guid memberId, CancellationToken cancellationToken = default)
        {
            var rooms = await _context.ChatRooms
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return rooms.Where(r => r.Members.Contains(memberId));
        }

        public async Task<IEnumerable<ChatRoom>> GetByCreatorAsync(Guid creatorId, CancellationToken cancellationToken = default)
        {
            return await _context.ChatRooms
                .AsNoTracking()
                .Where(r => r.Creator == creatorId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ChatRoom>> GetByTypeAsync(ChatRoomType type, CancellationToken cancellationToken = default)
        {
            return await _context.ChatRooms
                .AsNoTracking()
                .Where(r => r.ChatRoomType == type)
                .ToListAsync(cancellationToken);
        }

        public async Task<ChatRoom> AddAsync(ChatRoom chatRoom, CancellationToken cancellationToken = default)
        {
            await _context.ChatRooms.AddAsync(chatRoom, cancellationToken);
            return chatRoom;
        }

        public void Update(ChatRoom chatRoom)
        {
            _context.ChatRooms.Update(chatRoom);
        }

        public void Delete(ChatRoom chatRoom)
        {
            _context.ChatRooms.Remove(chatRoom);
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var room = await _context.ChatRooms
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

            if (room is not null)
            {
                _context.ChatRooms.Remove(room);
            }
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ChatRooms
                .AnyAsync(r => r.Id == id, cancellationToken);
        }
    }
}