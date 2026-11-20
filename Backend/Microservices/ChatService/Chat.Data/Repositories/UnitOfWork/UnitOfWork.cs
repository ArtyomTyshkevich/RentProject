using Chat.Applicaton.Interfaces;
using Chat.Data.Context;
using Chat.Domain.Enums;
using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Chat.Data.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _userDbContext;
        private readonly IConfiguration _configuration;

        public UnitOfWork(ChatDbContext libraryDbContext, IConfiguration configuration)
        {
            _userDbContext = libraryDbContext;
            _configuration = configuration;
            ChatRoom = new ChatRoomRepository(_userDbContext);
        }

        public IChatRoomRepository ChatRoom { get; private set; }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _userDbContext.SaveChangesAsync(cancellationToken);
        public void Dispose() => _userDbContext.Dispose();
    }
}