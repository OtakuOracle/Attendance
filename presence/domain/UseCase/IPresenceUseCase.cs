using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Models.ResponseModels;
using presence.domain.Models;

namespace domain.UseCase
{
    public interface IPresenceUseCase
    {
        List<PresenceResponse> GetPresence(int GroupId, DateOnly startData, DateOnly endData, int UserId);
        List<PresenceResponse> GetPresenceByGroup(int groupId);
        List<Presence> GetPresenceByGroupAndDate(int groupId, DateOnly date);
        bool UncheckAttendence(int firstClass, int lastClass, DateOnly date, int userId);
        void AddPresence(int firstClass, int lastClass, int groupId, DateOnly date);
        List<PresenceResponse> AddPresenceByDate(String startDate, String endDate, int groupId);
        Dictionary<string, int> GetPresenceStatsByGroup(int groupId);
        void GenerateWeeklyPresence(int firstClass, int lastClass, int groupId, DateOnly startDate);
        bool DeletePresenceByGroup(int groupId);
        bool DeletePresenceByUser(int UserId);
        bool DeletePresenceByDate(DateOnly startData, DateOnly endData);

    }
}