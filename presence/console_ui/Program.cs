using presence.data.Repository;
using presence.domain.UseCase;
using presence.ui;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using presence.data.RemoteData.RemoteDataBase;

IServiceCollection services = new ServiceCollection();

services
    .AddDbContext<RemoteDatabaseContext>()
    .AddSingleton<IGroupRepository, SQLGroupRepositoryImpl>()
    .AddSingleton<IUserRepository, SQLUserRepositoryImpl>()
    .AddSingleton<IPresenceRepository, SQLPresenceRepositoryImpl>()
    .AddSingleton<UserUseCase>()
    .AddSingleton<GroupUseCase>()
    .AddSingleton<PresenceUseCase>()
    .AddSingleton<PresenceConsoleUI>()
    .AddSingleton<MainMenuUI>();

// Создание провайдера сервисов
var serviceProvider = services.BuildServiceProvider();

// Получение экземпляра главного пользовательского интерфейса
MainMenuUI mainMenuUI = serviceProvider.GetService<MainMenuUI>();

// Вывод главного меню
mainMenuUI.DisplayMenu();