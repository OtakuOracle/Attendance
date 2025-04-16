using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.data.RemoteData.RemoteDataBase.DAO
{
    public class GroupDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<UserDao> User { get; set; }
    }
}
