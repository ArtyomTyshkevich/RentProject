using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.DAL.Entities;

namespace UserService.DAL.Data
{
    public class UserDbContext : IdentityDbContext<UserService.DAL.Entities.UserEntity, IdentityRole<Guid>, Guid>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
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