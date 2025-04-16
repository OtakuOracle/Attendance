using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.domain.Models
{
    public class Group
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}