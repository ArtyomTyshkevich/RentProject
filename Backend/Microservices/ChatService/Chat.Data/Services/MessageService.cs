using Chat.Domain.Models.Mongo.Base;
using MongoDB.Driver;

namespace Chat.Application.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<MessageBase> _messages;
        public MessageService(IMongoDatabase database)
        {
            _messages = database.GetCollection<MessageBase>("messages");
        }
        public async Task<List<MessageBase>> GetAllMessagesAsync()
        {
            return await _messages.Find(_ => true).ToListAsync();
        }

        public async Task SendMessageAsync(MessageBase message)
        {
            await _messages.InsertOneAsync(message);
        }
    }
}