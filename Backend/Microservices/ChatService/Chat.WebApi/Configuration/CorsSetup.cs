
namespace Chat.WebApi.Configuration
{
    public static class CorsSetup
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsPolicy = "_allowSignalRCors";

            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy, policy =>
                {
                    policy.WithOrigins("https://gourav-d.github.io")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

        }
    }
}