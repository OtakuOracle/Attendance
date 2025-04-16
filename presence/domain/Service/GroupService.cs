using data.Repository;
using domain.Models.ResponseModels;
using domain.Request;
using domain.UseCase;
using presence.data.RemoteData.RemoteDataBase.DAO;
using presence.data.Repository;
using presence.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Service
{
    public class GroupService : IGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        // Конструктор, инициализирующий репозиторий групп
        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public void AddGroup(AddGroupRequest addGroupRequest) //Метод для добавления новой группы
        {
            // Создаем новый объект GroupDao и добавляем его через репозиторий
            _groupRepository.AddGroup(new GroupDao { Name = addGroupRequest.Name });
        }

        public void AddGroupWithStudents(AddGroupWithStudentsRequest addGroupWithStudents) //Метод для добавления группы вместе со студентами
        {
            // Создаем объект GroupDao для группы
            GroupDao groupDAO = new GroupDao { Name = addGroupWithStudents.addGroupRequest.Name };

            // Преобразуем список студентов в список объектов UserDao
            List<UserDao> users = addGroupWithStudents
                .AddStudentRequests
                .Select(it => new UserDao { FIO = it.StudentName })
                .ToList();

            // Добавляем группу и студентов через репозиторий
            _groupRepository.AddStudents(groupDAO, users);
        }

        public IEnumerable<GroupResponse> GetGroupsWithStudents() //Метод для получения групп с их студентами синхронно
        {
            // Получаем все группы из репозитория
            return _groupRepository.GetAllGroup().Select(
                group => new GroupResponse
                {
                    Id = group.Id,
                    Name = group.Name,
                    User = group.User != null ? group.User.Select(
                        user => new UserResponse
                        {
                            Id = user.UserId,
                            FIO = user.FIO,
                            Group = new GroupResponse
                            {
                                Id = group.Id,
                                Name = group.Name,
                            }
                        }).ToList() : new List<UserResponse>() // Возвращаем пустой список, если нет студентов
                }).ToList();
        }

        public async Task<IEnumerable<GroupResponse>> GetGroupsWithStudentsAsync() // Асинхронный метод для получения групп с их студентами
        {
            // Запрашиваем все группы асинхронно из репозитория
            var result = await _groupRepository.getAllGroupAsync();

            // Преобразуем результаты в список GroupResponse с необходимыми данными
            return result.Select(
                group => new GroupResponse
                {
                    Id = group.Id,
                    Name = group.Name,
                    User = group.User.Select(
                        user => new UserResponse
                        {
                            Id = user.UserId,
                            FIO = user.FIO,
                            Group = new GroupResponse
                            {
                                Id = group.Id,
                                Name = group.Name,
                            }
                        }).ToList()
                }).ToList();
        }

        public void RemoveUserFromGroup(int UserId)
        {
            _userRepository.RemoveUserById(UserId);
        }

    }
}