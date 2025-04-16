using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using domain.Models.ResponseModels;
using presence.data.RemoteData.RemoteDataBase.DAO;
using presence.data.Repository;
using presence.domain.Models;

namespace presence.domain.UseCase
{
    public class PresenceUseCase
    {
        public readonly IUserRepository _userRepository;
        public readonly IPresenceRepository _presenceRepository;
        private readonly IGroupRepository _groupRepository;

        public PresenceUseCase(IPresenceRepository repositoryImpl,
                                IUserRepository userRepositoryImpl,
                                IGroupRepository groupRepositoryImpl)
        {
            _presenceRepository = repositoryImpl;
            _userRepository = userRepositoryImpl;
            _groupRepository = groupRepositoryImpl;
        }

        public List<PresenceResponse> GetPresence(int GroupId, DateOnly startData, DateOnly endData, int UserId) // Метод для получения присутствия пользователей в группе за определенный период времени
        {
            var users = _userRepository.GetAllUser().Where(x => x.GroupId == GroupId).ToList();

            var presence = _presenceRepository.GetPresence(GroupId, startData, endData, UserId)
                .Where(x => users.Any(user => user.UserId == x.UserId))
                .Select(presence => new PresenceResponse
                {
                    User = new UserResponse
                    {
                        Id = presence.UserId,
                        Group = new GroupResponse { Id = GroupId, Name = _groupRepository.GetGroupById(GroupId).Name },
                        FIO = users.First(user => user.UserId == presence.UserId).FIO,
                    },
                    ClassNumber = presence.ClassNumber,
                    Date = presence.Date,
                    IsAttendence = presence.IsAttendence
                }).ToList();
            return presence;
        }

        public List<PresenceResponse> GetPresenceByGroup(int groupId) // Метод для получения присутствия по группе
        {
            var users = _userRepository.GetAllUser().Where(x => x.GroupId == groupId).ToList();

            var presenceByGroup = _presenceRepository.GetPresenceByGroup(groupId)
                .Where(x => users.Any(user => user.UserId == x.UserId))
                .Select(presence => new PresenceResponse
                {
                    User = new UserResponse
                    {
                        Id = presence.UserId,
                        Group = new GroupResponse { Id = groupId, Name = _groupRepository.GetGroupById(groupId).Name },
                        FIO = users.First(user => user.UserId == presence.UserId).FIO,
                    },
                    ClassNumber = presence.ClassNumber,
                    Date = presence.Date,
                    IsAttendence = presence.IsAttendence
                }).ToList();
            return presenceByGroup;
        }

        public List<Presence> GetPresenceByGroupAndDate(int groupId, DateOnly date) // Метод для получения присутствия по группе и дате
        {
            var users = _userRepository.GetAllUser().Where(x => x.GroupId == groupId).ToList();

            var presenceByGroupAndDate = _presenceRepository.GetPresenceByGroupAndDate(groupId, date)
                .Where(x => users.Any(user => user.UserId == x.UserId && x.Date == date))
                .Select(presence => new Presence
                {
                    User = new User
                    {
                        Id = presence.UserId,
                        GroupId = new Group { Id = groupId, Name = _groupRepository.GetGroupById(groupId).Name },
                        FIO = users.First(user => user.UserId == presence.UserId).FIO,
                    },
                    ClassNumber = presence.ClassNumber,
                    Date = presence.Date,
                    IsAttendence = presence.IsAttendence
                }).ToList();
            return presenceByGroupAndDate;
        }

        public bool UncheckAttendence(int firstClass, int lastClass, DateOnly date, int userId) // Метод для отмены отметки о посещении
        {
            return _presenceRepository.UncheckAttendence(firstClass, lastClass, date, userId);
        }

        public void AddPresence(int firstClass, int lastClass, int groupId, DateOnly date) //Метод для добавления посещения
        {
            var users = _userRepository.GetAllUser().Where(x => x.GroupId == groupId).ToList();
            List<Presence> presenceList = new List<Presence>();
            for (int i = firstClass; i < lastClass; i++)
            {
                foreach (var user in users)
                {
                    Presence pres = new Presence
                    {
                        ClassNumber = i,
                        Date = date,
                        User = new User
                        {
                            Id = user.UserId,
                            FIO = user.FIO,
                            GroupId = new Group
                            {
                                Id = groupId,
                                Name = _groupRepository.GetGroupById(groupId).Name
                            }
                        }
                    };
                    presenceList.Add(pres);
                }
            }
        }

        public List<PresenceResponse> AddPresenceByDate(String startDate, String endDate, int groupId) // Метод для добавления посещаемости на заданный диапазон дат
        {
            var users = _userRepository.GetAllUser().Where(x => x.GroupId == groupId).ToList();
            List<PresenceResponse> presenceList = new List<PresenceResponse>();

            for (var currentDate = DateOnly.Parse(startDate); currentDate <= DateOnly.Parse(endDate); currentDate = currentDate.AddDays(1))
            {
                for (int i = 1; i < 8; i++)
                {
                    if (currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        continue;
                    }

                    foreach (var user in users)
                    {
                        var presenceDao = new PresenceDao
                        {
                            ClassNumber = i,
                            Date = currentDate,
                            UserId = user.UserId,
                            User = user,
                            GroupId = groupId
                        };
                        _presenceRepository.AddPresence(presenceDao);
                    }
                }

            }
            return presenceList;
        }

        public Dictionary<string, int> GetPresenceStatsByGroup(int groupId) // Метод для получения статистики посещаемости по группе
        {
            var stats = new Dictionary<string, int>();


            // Получаем всех студентов группы
            var users = _userRepository.GetAllUser().Where(x => x.GroupId == groupId).ToList();
            stats["Количество студентов"] = users.Count;

            // Получаем все записи посещаемости для группы
            var presences = _presenceRepository.GetPresenceByGroup(groupId);

            // Считаем количество уникальных занятий
            var uniqueLessons = presences
                .Select(p => new { p.Date, p.ClassNumber })
                .Distinct()
                .Count();
            stats["Количество занятий"] = uniqueLessons;

            // Считаем общую посещаемость
            var totalAttendances = presences.Count(p => p.IsAttendence);
            var totalPossibleAttendances = users.Count * uniqueLessons;

            if (totalPossibleAttendances > 0)
            {
                var attendancePercentage = (totalAttendances * 100) / totalPossibleAttendances;
                stats["Процент посещаемости"] = attendancePercentage;
            }
            else
            {
                stats["Процент посещаемости"] = 0;
            }

            return stats;
        }

        public void GenerateWeeklyPresence(int firstClass, int lastClass, int groupId, DateOnly startDate) // Метод для генерации посещаемости на неделю для заданной группы
        {
            var users = _userRepository.GetAllUser().Where(x => x.GroupId == groupId).ToList();
            for (int day = 0; day < 7; day++)
            {
                var currentDate = startDate.AddDays(day);

                // Пропускаем субботу и воскресенье
                if (currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }

                for (int classNum = firstClass; classNum <= lastClass; classNum++)
                {
                    foreach (var user in users)
                    {
                        var presence = new PresenceDao
                        {
                            ClassNumber = classNum,
                            Date = currentDate,
                            UserId = user.UserId,
                            User = user,
                            GroupId = groupId
                        };

                        _presenceRepository.AddPresence(presence);
                    }
                }
            }
        }

        public bool DeletePresenceByGroup(int groupId) // Метод для удаления всех записей посещаемости по идентификатору группы
        {
            return _presenceRepository.DeletePresenceByGroup(groupId);
        }

        public bool DeletePresenceByDate(DateOnly startData, DateOnly endData) // Метод для удаления посещения по дате 
        {
            return _presenceRepository.DeletePresenceByDate(startData, endData);
        }

        public bool DeletePresenceByUser(int UserId) // Метод для удаления посещения по юзеру
        {
            return _presenceRepository.DeletePresenceByUser(UserId);
        }
    }
}
