using Chat.Domain.Settings;
using MongoDB.Driver;
using System.Reflection.Metadata;

namespace Chat.WebApi.Configuration
{
    public static class MongoDatabaseSetup
    {
        public static void ConfigureMongoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Привязываем настройки (как было)
            services.Configure<BookStoreDatabaseSettings>(
                configuration.GetSection("BookStoreDatabase"));

            // 2. Регистрируем IMongoClient (Singleton — это важно!)
            services.AddSingleton<IMongoClient>(sp =>
            {
                var settings = configuration.GetSection("BookStoreDatabase").Get<BookStoreDatabaseSettings>();
                return new MongoClient(settings.ConnectionString);
            });

            // 3. Регистрируем IMongoDatabase (Scoped)
            services.AddScoped<IMongoDatabase>(sp =>
            {
                var settings = configuration.GetSection("BookStoreDatabase").Get<BookStoreDatabaseSettings>();
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(settings.DatabaseName);
            });
        }
    }
}