using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using presence.data.RemoteData;
using presence.data.RemoteData.RemoteDataBase;
using presence.data.RemoteData.RemoteDataBase.DAO;
using presence.data.Repository;

namespace data.Repository
{
    public class AdminRepositoryImp : IAdminRepository
    {
        private readonly RemoteDatabaseContext _remoteDataBaseContext;

        // Конструктор, принимающий контекст базы данных
        public AdminRepositoryImp(RemoteDatabaseContext remoteDataBaseContext)
        {
            _remoteDataBaseContext = remoteDataBaseContext;
        }

        public UserDao GetStudentInfo(int userId) // Метод для получения информации о студенте по его идентификатору
        {
            return _remoteDataBaseContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public bool AddStudents(GroupDao group, List<UserDao> students) // Метод для добавления студентов в группу
        {
            // Добавляем группу в контекст
            _remoteDataBaseContext.Groups.Add(group);
            _remoteDataBaseContext.SaveChanges(); // Сохраняем изменения в базе данных

            // Добавляем студентов в контекст
            foreach (UserDao student in students)
            {
                _remoteDataBaseContext.Users.Add(student);
            }
            _remoteDataBaseContext.SaveChanges(); // Сохраняем изменения в базе данных

            return true; // Возвращаем true, если добавление прошло успешно
        }

        public bool RemoveUserById(int userId, int groupId) // Метод для удаления пользователя по его идентификатору и идентификатору группы
        {
            // Находим пользователя в контексте
            var userLocal = _remoteDataBaseContext.Users
                .Where(x => x.UserId == userId && x.GroupId == groupId).FirstOrDefault();

            if (userLocal == null) return false;

            // Удаляем пользователя из контекста
            _remoteDataBaseContext.Users.Remove(userLocal);
            _remoteDataBaseContext.SaveChanges(); // Сохраняем изменения в базе данных

            return true;
        }

        public IEnumerable<GroupDao> GetAllGroupsWithStudents() // Метод для получения всех групп с их студентами
        {
            // Получаем группы и их студентов
            return _remoteDataBaseContext.Groups.Select(x => new GroupDao
            {
                Id = x.Id, // Идентификатор группы
                Name = x.Name, // Название группы
                User = _remoteDataBaseContext.Users.Where(it => it.GroupId == x.Id).ToList() // Список студентов в группе
            }).ToList();
        }
    }
}
