
using Chat.Domain.Models.Mongo.Base;

namespace Chat.Domain.Models.Mongo
{

    public class SystemMessage : MessageBase
    {
        public string SystemText { get; set; } = null!;
    }
}
