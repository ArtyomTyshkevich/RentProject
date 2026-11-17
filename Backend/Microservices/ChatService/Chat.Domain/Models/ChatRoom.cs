
using Chat.Domain.Enums;

namespace Chat.Domain.Models
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public Guid Creator {  get; set; }
        public List<Guid> Members { get; set; }
        public string Name { get; set; }
        public ChatRoomType ChatRoomType { get; set; }
    }
}
