using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.data.LocalData.Entity
{
    public class PresenceLocalEntity
    {
        public required DateOnly Date { get; set; }
        public int ClassNumber { get; set; }
        public bool IsAttendence { get; set; } = true;
        public required Guid UserGuid { get; set; }
    }
}