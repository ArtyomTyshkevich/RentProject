using UserService.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.DAL.DI
{
    public static class DatabaseSetup
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}