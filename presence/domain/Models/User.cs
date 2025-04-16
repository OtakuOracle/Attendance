using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.domain.Models
{
    public class User
    {
        public required string FIO { get; set; }
        public int Id { get; set; }
        public required Group GroupId { get; set; }
    }
}