using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace domain.Models.ResponseModels
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserResponse> User { get; set; }
    }
}