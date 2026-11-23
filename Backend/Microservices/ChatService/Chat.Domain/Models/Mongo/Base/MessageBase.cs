using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chat.Domain.Models.Mongo.Base
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(TextMessage), typeof(MediaMessage), typeof(SystemMessage))]
    public abstract class MessageBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public Guid ChatRoomId { get; set; }
        public Guid SenderId { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
    }
}
