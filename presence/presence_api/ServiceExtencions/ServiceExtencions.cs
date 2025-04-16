using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using data.Repository;
using domain.UseCase;
using presence.data.Repository;
using presence.domain.UseCase;

namespace presence_api.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureGroup(this IServiceCollection services)
        {
            services
            .AddScoped<IGroupRepository, SQLGroupRepositoryImpl>()
            .AddScoped<GroupUseCase>();
        }

        public static void ConfigureUser(this IServiceCollection services)
        {
            services
            .AddScoped<IUserRepository, SQLUserRepositoryImpl>()
            .AddScoped<UserUseCase>();
        }

        public static void ConfigurePresence(this IServiceCollection services)
        {
            services
            .AddScoped<IPresenceRepository, SQLPresenceRepositoryImpl>()
            .AddScoped<PresenceUseCase>();
        }

        public static void ConfigureAdmin(this IServiceCollection services)
        {
            services
            .AddScoped<IUserRepository, SQLUserRepositoryImpl>()
            .AddScoped<IGroupRepository, SQLGroupRepositoryImpl>()
            .AddScoped<IPresenceRepository, SQLPresenceRepositoryImpl>()
            .AddScoped<IAdminRepository, AdminRepositoryImp>()
            .AddScoped<UserUseCase>()
            .AddScoped<GroupUseCase>()
            .AddScoped<PresenceUseCase>()
            .AddScoped<AdminUseCase>();
        }
    }
}