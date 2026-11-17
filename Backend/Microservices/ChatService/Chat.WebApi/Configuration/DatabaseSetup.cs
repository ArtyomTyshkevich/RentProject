using Chat.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.WebApi.Configuration
{
    public static class DatabaseSetup
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!));
        }
    }
}
