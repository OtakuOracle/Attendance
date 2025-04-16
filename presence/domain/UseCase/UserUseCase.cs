using domain.Models.ResponseModels;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase.DAO;
using presence.data.Repository;
using presence.domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.domain.UseCase
{
    public class UserUseCase
    {


        private readonly IUserRepository _repositoryUserImpl;
        private readonly IGroupRepository _repositoryGroupImpl;

        public UserUseCase(IUserRepository repositoryImpl, IGroupRepository repositoryGroupImpl)
        {
            _repositoryUserImpl = repositoryImpl;
            _repositoryGroupImpl = repositoryGroupImpl;
        }

        public List<User> GetAllUsers() => _repositoryUserImpl.GetAllUser() // Метод для получения списка всех пользователей с соответствующими группами
            .Join(_repositoryGroupImpl.GetAllGroup(),
            user => user.GroupId,
            group => group.Id,
            (user, group) =>
            new User
            {
                FIO = user.FIO,
                Id = user.UserId,
                GroupId = new Group { Id = group.Id, Name = group.Name }
            }
            ).ToList();

        public User GetUserById(int userId) //Метод для получения пользователя по его Id
        {
            return new User
            {
                FIO = _repositoryUserImpl.GetUserById(userId).FIO,
                Id = _repositoryUserImpl.GetUserById(userId).UserId,
                GroupId = new Group { Id = _repositoryUserImpl.GetUserById(userId).GroupId, Name = _repositoryGroupImpl.GetGroupById(userId).Name }
            };
        }

        public bool UpdateUser(User user) //Метод для обновления пользователя
        {
            UserDao userDao = new UserDao { FIO = user.FIO, GroupId = user.GroupId.Id };
            return _repositoryUserImpl.UpdateUser(userDao);
        }

        public bool UpdateUserById(int userId, String fio, int groupId) //Метод для обновления пользователя по его Id 
        {
            UserDao userDao = new UserDao { FIO = fio, GroupId = groupId };
            return _repositoryUserImpl.UpdateUser(userDao);
        }

        public bool RemoveUserById(int userId) //Метод для удаления пользователя по его Id
        {
            return _repositoryUserImpl.RemoveUserById(userId);
        }

    }
}