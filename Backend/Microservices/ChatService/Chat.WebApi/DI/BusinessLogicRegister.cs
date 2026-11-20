using Chat.WebApi.Configuration;

namespace Chat.WebApi.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);
        }
    }
}
