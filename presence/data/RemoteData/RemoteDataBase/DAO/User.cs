using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.data.RemoteData.RemoteDataBase.DAO
{
    public class UserDao
    {
        public required string FIO { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public GroupDao Group { get; set; }
        public IEnumerable<PresenceDao> Presences { get; set; }
    }
}