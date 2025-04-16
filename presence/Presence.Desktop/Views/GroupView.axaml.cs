using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using Presence.Desktop.ViewModels;
using ReactiveUI;

namespace Presence.Desktop.Views
{
    public partial class GroupView : ReactiveUserControl<GroupViewModel>
    {
        public GroupView()
        {
            this.WhenActivated(action =>
            {
                action(ViewModel!.SelectFileInteraction.RegisterHandler(ShowFileDialog));
            });
            AvaloniaXamlLoader.Load(this);
        }

        private async Task ShowFileDialog(IInteractionContext<string?, string?> context)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            var storageFile = await topLevel.StorageProvider.OpenFilePickerAsync(
                new FilePickerOpenOptions()
                {
                    AllowMultiple = false,
                    Title = context.Input
                }
            );
        }
    }
}