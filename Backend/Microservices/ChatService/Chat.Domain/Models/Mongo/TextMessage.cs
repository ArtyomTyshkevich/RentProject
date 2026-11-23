
using Chat.Domain.Models.Mongo.Base;

namespace Chat.Domain.Models.Mongo
{   // Базовый класс для всех сообщений

    public class TextMessage : MessageBase
    {
        public string Text { get; set; } = null!;
    }
}
