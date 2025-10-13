using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.DAL.Entities;

namespace UserService.DAL.Data
{
    public class AuthDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Pros> Pros { get; set; }

    }
}