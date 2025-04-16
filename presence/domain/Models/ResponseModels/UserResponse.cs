using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models.ResponseModels
{
    public class UserResponse
    {
        public required string FIO { get; set; }
        public int Id { get; set; }
        public int GroupId { get; set; }
        public GroupResponse Group { get; set; }
    }
}
