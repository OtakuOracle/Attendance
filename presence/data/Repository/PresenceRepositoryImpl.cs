using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using presence.data.LocalData;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase;
using presence.data.RemoteData.RemoteDataBase.DAO;

namespace presence.data.Repository
{
    public class SQLPresenceRepositoryImpl : IPresenceRepository
    {
        private readonly RemoteDatabaseContext _remoteDatabaseContext;

        public SQLPresenceRepositoryImpl(RemoteDatabaseContext remoteDatabaseContext)
        {
            _remoteDatabaseContext = remoteDatabaseContext;
        }

        public bool AddPresence(PresenceDao presence) //Метод добавления посещения
        {
            var presenceDao = new PresenceDao
            {
                Date = presence.Date,
                ClassNumber = presence.ClassNumber,
                UserId = presence.UserId,
                GroupId = presence.GroupId
            };
            _remoteDatabaseContext.Presences.Add(presenceDao);
            _remoteDatabaseContext.SaveChanges();
            return true;
        }

        public List<PresenceDao> GetPresence(int GroupId, DateOnly startData, DateOnly endData, int UserId) // Метод для получения присутствий по группе и диапазону дат
        {
            var presenceList = _remoteDatabaseContext.Presences.Where(presence => presence.GroupId == GroupId
            && presence.UserId == UserId && presence.Date >= startData && presence.Date <= endData).ToList();
            return presenceList;
        }

        public List<PresenceDao> GetPresenceByGroup(int groupId)  // Метод для получения всех присутствий по ID группы
        {
            var listPresences = _remoteDatabaseContext.Presences
            .Where(x => x.GroupId == groupId).ToList();
            return listPresences;
        }

        public List<PresenceDao> GetPresenceByGroupAndDate(int groupId, DateOnly date) // Метод для получения присутствий по группе и дате
        {
            var listPresences = _remoteDatabaseContext.Presences
            .Where(x => x.GroupId == groupId && x.Date == date).ToList();
            return listPresences;
        }

        public bool DeletePresenceByGroup(int groupId) // Метод для удаления всех присутствий по ID группы
        {
            var presenceToDelete = _remoteDatabaseContext.Presences.Where(x => x.GroupId == groupId).ToList();

            if (presenceToDelete.Count > 0)
            {
                foreach (var presence in presenceToDelete)
                {
                    _remoteDatabaseContext.Presences.Remove(presence);
                }

                _remoteDatabaseContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePresenceByUser(int userId) // Метод для удаления присутствий по ID пользователя
        {
            var presenceToDelete = _remoteDatabaseContext.Presences.Where(x => x.UserId == userId).ToList();

            if (presenceToDelete.Count > 0)
            {
                foreach (var presence in presenceToDelete)
                {
                    _remoteDatabaseContext.Presences.Remove(presence);
                }

                _remoteDatabaseContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeletePresenceByDate(DateOnly startData, DateOnly endData) // Метод для удаления присутствий в заданном диапазоне дат
        {
            var PresenceToDelete = _remoteDatabaseContext.Presences.Where(x => x.Date == startData).ToList();
            for (var i = startData; i < endData; i = i.AddDays(1))
            {
                PresenceToDelete.AddRange(_remoteDatabaseContext.Presences.Where(x => x.Date == i).ToList());
            }
            if (PresenceToDelete.Count > 0)
            {
                foreach (var presence in PresenceToDelete)
                {
                    _remoteDatabaseContext.Presences.Remove(presence);
                }

                _remoteDatabaseContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool UncheckAttendence(int firstClass, int lastClass, DateOnly date, int userId) // Метод для отмены регистрации посещаемости в заданном диапазоне классов
        {
            var presToUpdate = _remoteDatabaseContext.Presences
                .Where(x => x.UserId == userId && x.ClassNumber >= firstClass
                        && x.ClassNumber <= lastClass && x.Date == date).ToList();

            foreach (var pres in presToUpdate)
            {
                pres.IsAttendence = false;
            }
            _remoteDatabaseContext.SaveChanges();
            return true;
        }
    }
}