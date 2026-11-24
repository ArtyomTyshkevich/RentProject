
using UserService.DAL.Enums;

namespace User.BLL.DTOs.Review

{
    public class ProsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ProsTypes ProsType { get; set; }
    }
}
