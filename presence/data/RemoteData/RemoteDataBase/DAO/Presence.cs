using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.data.RemoteData.RemoteDataBase.DAO
{
    public class PresenceDao
    {
        public int PresenceId { get; set; }
        public DateOnly Date { get; set; }
        public int ClassNumber { get; set; }
        public bool IsAttendence { get; set; } = true;
        public int UserId { get; set; }
        public UserDao User { get; set; }
        public int GroupId { get; set; }
    }
}