using Chat.Domain.Models.Mongo.Base;

namespace Chat.Domain.Models.Mongo
{
    public class MediaMessage : MessageBase
    {
        public string MediaType { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int? Width { get; set; }
        public int? Height { get; set; }
        public long? SizeInBytes { get; set; }
    }

}
