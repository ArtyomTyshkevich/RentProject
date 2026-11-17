
namespace User.BLL.DTOs.Review
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Rating { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
