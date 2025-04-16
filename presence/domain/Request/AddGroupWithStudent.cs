using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Request
{
    public class AddGroupWithStudentsRequest
    {
        public AddGroupRequest addGroupRequest { get; set; }
        public IEnumerable<AddStudentRequest> AddStudentRequests { get; set; }
    }
}
