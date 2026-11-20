using Chat.Domain.Enums;

namespace Chat.Application.DTOs
{
    public class ChatRoomDto
    {
        public Guid Id { get; set; }
        public Guid Creator { get; set; }
        public List<Guid> Members { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public ChatRoomType ChatRoomType { get; set; }
    }
}