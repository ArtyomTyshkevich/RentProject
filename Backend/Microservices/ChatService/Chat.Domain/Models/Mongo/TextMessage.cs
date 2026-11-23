using Chat.Domain.Models.Mongo.Base;

namespace Chat.Domain.Models.Mongo
{
    public class TextMessage : MessageBase
    {
        public string Text { get; set; } = null!;
    }
}
