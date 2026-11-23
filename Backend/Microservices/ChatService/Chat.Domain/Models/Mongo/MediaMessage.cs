
using Chat.Domain.Models.Mongo.Base;

namespace Chat.Domain.Models.Mongo
{

    public class MediaMessage : MessageBase
    {
        public string MediaType { get; set; } = null!; // "photo", "video", "sticker"
        public string Url { get; set; } = null!;

        // Дополнительные метаданные для красивого отображения на фронте
        public int? Width { get; set; }
        public int? Height { get; set; }
        public long? SizeInBytes { get; set; }
    }

}
