using data;
using data.Repository;
using domain.Service;
using domain.UseCase;
using Microsoft.Extensions.DependencyInjection;
using presence.data.RemoteData.RemoteDataBase;
using presence.data.Repository;
using presence.domain.UseCase;
using Presence.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Presence.Desktop.DI
{
    public static class ServiceColletionExtensions
    {
        public static void AddCommonService(this IServiceCollection collection)
        {
            collection
             .AddDbContext<RemoteDatabaseContext>()
             .AddSingleton<IGroupRepository, SQLGroupRepositoryImpl>()
             .AddTransient<IGroupUseCase, GroupService>()
             .AddTransient<GroupViewModel>()
             .AddSingleton<IUserRepository, SQLUserRepositoryImpl>();
        }
    }
}