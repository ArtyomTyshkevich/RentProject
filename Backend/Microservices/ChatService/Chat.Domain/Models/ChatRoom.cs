using System.ComponentModel.DataAnnotations.Schema;
using Chat.Domain.Enums;

namespace Chat.Domain.Models
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public Guid Creator { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public List<Guid> Members { get; set; } = new();

        public string Name { get; set; }
        public ChatRoomType ChatRoomType { get; set; }
    }
}