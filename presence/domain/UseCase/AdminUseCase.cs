using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using data.Repository;
using domain.Models.ResponseModels;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase.DAO;
using presence.data.Repository;

namespace domain.UseCase
{
    public class AdminUseCase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IGroupRepository _groupRepository;

        // Конструктор, инициализирующий оба репозитория
        public AdminUseCase(IAdminRepository adminRepository, IGroupRepository groupRepository)
        {
            _adminRepository = adminRepository;
            _groupRepository = groupRepository;
        }

        // Метод для получения всех групп с их студентами
        public IEnumerable<GroupResponse> GetAllGroupsWithStudents() =>
            _adminRepository.GetAllGroupsWithStudents().Select(it => new GroupResponse
            {
                Name = it.Name,
                Id = it.Id,
                // Фильтруем пользователей по идентификатору группы и создаем список UserResponse
                User = it.User.Where(u => u.GroupId == it.Id)
                        .Select(u => new UserResponse { Id = u.UserId, FIO = u.FIO })
                        .ToList()
            })
                .ToList();

        public bool AddStudents(string GroupName, List<string> Students) //Метод для добавления студента в существующую группу
        {
            // Создаем объект GroupDao для группы
            GroupDao groupDao = new GroupDao { Name = GroupName };
            List<UserDao> users = new List<UserDao>();

            // Проходим по списку студентов и создаем объекты UserDao
            foreach (string student in Students)
            {
                var user = new UserDao
                {
                    FIO = student,
                    Group = groupDao // Привязываем студента к группе
                };
                users.Add(user);
            }

            // Добавляем студентов в репозиторий и возвращаем результат операции
            return _adminRepository.AddStudents(groupDao, users);
        }

        public UserResponse GetStudentInfo(int userId) //Метод для получения информации о студенте по его Id 
        {
            // Запрашиваем информацию о студенте из репозитория
            var studentInfo = _adminRepository.GetStudentInfo(userId);

            // Если информация не найдена, возвращаем null
            if (studentInfo == null) return null;

            // Создаем объект UserResponse с данными студента
            UserResponse user = new UserResponse
            {
                Id = userId,
                FIO = studentInfo.FIO,
                GroupId = studentInfo.GroupId
            };
            return user;
        }

        // Метод для удаления группы по идентификатору
        public bool DeleteGroup(int groupId) =>
            _groupRepository.RemoveGroupById(groupId);


        // Метод для удаления пользователя из группы по их идентификаторам
        public bool DeleteUserFromGroup(int userId, int groupId) =>
            _adminRepository.RemoveUserById(userId, groupId);
    }

}
