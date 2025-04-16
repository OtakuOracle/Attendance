using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Presence.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();

        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            var groupViewModel = serviceProvider.GetRequiredService<GroupViewModel>();
            Router.Navigate.Execute(groupViewModel);
        }
    }
}