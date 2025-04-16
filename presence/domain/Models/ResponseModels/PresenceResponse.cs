using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models.ResponseModels
{
    public class PresenceResponse
    {
        public required DateOnly Date { get; set; }
        public int ClassNumber { get; set; }
        public bool IsAttendence { get; set; }
        public required UserResponse User { get; set; }
    }
}