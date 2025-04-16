using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using presence.data.RemoteData.RemoteDataBase.DAO;

namespace data.Repository
{
    public interface IAdminRepository
    {
        bool AddStudents(GroupDao group, List<UserDao> students);
        IEnumerable<GroupDao> GetAllGroupsWithStudents();
        UserDao GetStudentInfo(int userId);
        bool RemoveUserById(int userId, int groupId);
    }
}