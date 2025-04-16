using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using presence.data.Entity;
using presence.data.LocalData;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase;
using presence.data.RemoteData.RemoteDataBase.DAO;

namespace presence.data.Repository
{
    public class SQLGroupRepositoryImpl : IGroupRepository
    {
        private readonly RemoteDatabaseContext _remoteDatabaseContext;

        // Конструктор для инициализации контекста базы данных
        public SQLGroupRepositoryImpl(RemoteDatabaseContext remoteDatabaseContext)
        {
            _remoteDatabaseContext = remoteDatabaseContext;
        }

        public async Task<IEnumerable<GroupDao>> getAllGroupAsync() // Метод для получения всех групп
        {
            // Получаем список всех групп и связанных пользователей асинхронно
            return await _remoteDatabaseContext.Groups
                .Include(group => group.User)
                .ToListAsync();
        }
        public List<GroupDao> GetAllGroup() // Метод для получения всех групп
        {
            return _remoteDatabaseContext.Groups
                .Include(g => g.User)
                .Select(g => new GroupDao
                {
                    Name = g.Name,
                    Id = g.Id,
                    User = g.User.Select(u => new UserDao
                    {
                        FIO = u.FIO,
                        UserId = u.UserId,
                        GroupId = u.GroupId
                    }).ToList() // Формируем список пользователей группы
                }).ToList();
        }

        public GroupDao GetGroupById(int groupID) // Метод для получения группы по ID
        {
            var groupLocal = _remoteDatabaseContext.Groups
                                .Where(g => g.Id == groupID).FirstOrDefault();

            return groupLocal;
        }


        public bool AddGroup(GroupDao group) // Метод для добавления новой группы
        {
            // Создаем новый объект группы с указанным именем
            var groupDao = new GroupDao
            {
                Name = group.Name
            };

            // Добавляем группу в контекст и сохраняем изменения
            _remoteDatabaseContext.Groups.Add(groupDao);
            _remoteDatabaseContext.SaveChanges();
            return true;
        }

        public bool RemoveGroupById(int groupID) // Метод для удаления группы по ID
        {
            // Ищем группу по заданному ID
            var groupLocal = _remoteDatabaseContext.Groups
                .Where(x => x.Id == groupID).FirstOrDefault();

            // Если группа не найдена, возвращаем false
            if (groupLocal == null) return false;

            // Ищем пользователей, связанные с группой
            var userLocal = _remoteDatabaseContext.Users
                .Where(x => x.GroupId == groupID).ToList();

            // Если пользователи не найдены, это не ошибка
            foreach (var user in userLocal)
            {
                _remoteDatabaseContext.Users.Remove(user); // Удаляем каждого пользователя
            }

            // Удаляем саму группу
            _remoteDatabaseContext.Groups.Remove(groupLocal);
            _remoteDatabaseContext.SaveChanges(); // Сохраняем изменения
            return true;
        }

        public bool AddStudents(GroupDao group, List<UserDao> students)// Метод для добавления студентов в группу
        {
            // Добавляем группу в контекст
            _remoteDatabaseContext.Groups.Add(group);
            _remoteDatabaseContext.SaveChanges(); // Сохраняем изменения

            // Добавляем студентов в контекст
            foreach (UserDao student in students)
            {

                _remoteDatabaseContext.Users.Add(student);
            }

            // Сохраняем изменения после добавления студентов
            _remoteDatabaseContext.SaveChanges();
            return true;
        }

        public bool UpdateGroupById(int groupID, string name) // Метод для обновления группы по ID
        {
            // Ищем группу по ID и загружаем связанных пользователей
            var groupLocal = _remoteDatabaseContext.Groups
                .Include(g => g.User)
                .Where(x => x.Id == groupID).FirstOrDefault();

            if (groupLocal == null) return false;
            groupLocal.Name = name;

            groupLocal.User = _remoteDatabaseContext.Users
                .Where(x => x.GroupId == groupLocal.Id)
                .Select(user => new UserDao
                {
                    UserId = user.UserId,
                    FIO = user.FIO,
                    GroupId = user.GroupId
                }).ToList();

            _remoteDatabaseContext.SaveChanges(); // Сохраняем изменения
            return true;
        }
    }
}