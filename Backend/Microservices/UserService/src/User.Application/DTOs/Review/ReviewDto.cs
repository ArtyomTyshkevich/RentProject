
namespace User.BLL.DTOs.Review

{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public double Score { get; set; }
        public string? Text { get; set; }
        public List<ProsDto> Pros { get; set; } = new();
        public UserDto Reviewer { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
