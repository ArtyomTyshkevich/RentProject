using FluentValidation.AspNetCore;
using Library.Application.Interfaces;
using Library.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.BLL.Interfaces;
using User.BLL.Repositories;
using UserService.BLL.Interfaces;
using UserService.BLL.Mapping.Profiles;
using UserService.BLL.Repositories.UnitOfWork;
using UserService.DAL.DI;
using UserService.DAL.Repositories;

namespace UserService.BLL.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.ConfigureAuthentication(configuration);
            services.ConfigureSwagger();
            services.ConfigureDatabase(configuration);
            services.ConfigureIdentity();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IProsRepository, ProsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(ProsMappingProfile));
        }
    }
}
