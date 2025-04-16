using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.domain.Models
{
    public class Presence
    {
        public required DateOnly Date { get; set; }
        public int ClassNumber { get; set; }
        public bool IsAttendence { get; set; }
        public required User User { get; set; }
    }
}