using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Presence.Desktop.DI;
using Presence.Desktop.ViewModels;
using Presence.Desktop.Views;

namespace Presence.Desktop
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCommonService();
            var services = serviceCollection.BuildServiceProvider();
            var mainViewModel = services.GetRequiredService<GroupViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowViewModel(services),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

    }
}