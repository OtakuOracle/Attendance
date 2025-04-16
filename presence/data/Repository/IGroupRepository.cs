using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using presence.data.Entity;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase.DAO;

namespace presence.data.Repository
{
    public interface IGroupRepository
    {
        List<GroupDao> GetAllGroup();
        bool RemoveGroupById(int groupID);
        bool UpdateGroupById(int groupID, String name);
        GroupDao GetGroupById(int groupID);
        bool AddGroup(GroupDao group);
        bool AddStudents(GroupDao group, List<UserDao> students);
        public Task<IEnumerable<GroupDao>> getAllGroupAsync();
    }
}