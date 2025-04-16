using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Presence.Desktop.Models;
using Presence.Desktop.ViewModels;
using ReactiveUI;

namespace Presence.Desktop.Views;

public partial class PresenceView : ReactiveUserControl<PresenceModel>
{
    public PresenceView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }

}