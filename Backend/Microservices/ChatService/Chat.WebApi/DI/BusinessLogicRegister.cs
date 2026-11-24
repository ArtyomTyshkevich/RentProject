using Chat.Application.Services;
using Chat.Application.Services.Implementation;
using Chat.Applicaton.Interfaces;
using Chat.Applicaton.Mappers;
using Chat.Data.Repositories;
using Chat.Data.Repositories.UnitOfWork;
using Chat.WebApi.Configuration;

namespace Chat.WebApi.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);
            services.ConfigureMongoDatabase(configuration);
            services.ConfigureCors(configuration);
            services.AddAutoMapper(cfg => { }, typeof(ChatRoomProfile));
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<MessageService>();
            services.AddSignalR();
        }
    }
}
