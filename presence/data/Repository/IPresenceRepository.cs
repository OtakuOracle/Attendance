using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase.DAO;

namespace presence.data.Repository
{
    public interface IPresenceRepository
    {
        List<PresenceDao> GetPresence(int GroupId, DateOnly startData, DateOnly endData, int UserId);
        List<PresenceDao> GetPresenceByGroup(int groupId);
        List<PresenceDao> GetPresenceByGroupAndDate(int groupId, DateOnly date);
        bool UncheckAttendence(int firstClass, int lastClass, DateOnly date, int userId);
        bool AddPresence(PresenceDao presence);
        bool DeletePresenceByGroup(int groupId);
        bool DeletePresenceByUser(int userId);
        bool DeletePresenceByDate(DateOnly startData, DateOnly endData);


    }
}