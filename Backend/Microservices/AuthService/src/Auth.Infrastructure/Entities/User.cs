using Microsoft.AspNetCore.Identity;

namespace UserService.DAL.Entities
{

    public class User : IdentityUser<Guid>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public double Rating { get; set; }
        public List<Review> Reviews { get; set; } = new();
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}