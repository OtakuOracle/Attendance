using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase.DAO;

namespace presence.data.Repository
{
    public interface IUserRepository
    {
        List<UserDao> GetAllUser();
        bool RemoveUserById(int usesId);
        UserDao GetUserById(int userId);
        bool UpdateUser(UserDao userUpdate);
        bool UpdateUserById(int userId, string fio, int groupId);
    }
}