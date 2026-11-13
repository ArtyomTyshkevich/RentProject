
namespace UserService.DAL.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public double Score { get; set; }
        public string? Text { get; set; }
        public List<Pros> Pros { get; set; } = new();
        public UserEntity Reviewer { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
