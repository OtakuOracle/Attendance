using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presence.Desktop.Models
{
    public class GroupPresenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserPresenter>? Users { get; set; } = null;
    }
}