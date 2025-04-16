using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using presence.data.LocalData;
using presence.data.LocalData.Entity;
using presence.data.RemoteData.RemoteDataBase;
using presence.data.RemoteData.RemoteDataBase.DAO;

namespace presence.data.Repository
{
    public class SQLUserRepositoryImpl : IUserRepository
    {
        private readonly RemoteDatabaseContext _remoteDatabaseContext;

        // Конструктор для инициализации контекста базы данных
        public SQLUserRepositoryImpl(RemoteDatabaseContext remoteDatabaseContext)
        {
            _remoteDatabaseContext = remoteDatabaseContext;
        }

        public List<UserDao> GetAllUser() //Метод для получения списка всех пользователей
        {
            // Запрашиваем всех пользователей и преобразуем их в список UserDao
            return _remoteDatabaseContext.Users.Select(u => new UserDao
            {
                FIO = u.FIO,
                UserId = u.UserId,
                GroupId = u.GroupId
            }).ToList();
        }

        public UserDao GetUserById(int userId) //Метод для получения пользователя по его Id
        {
            // Находим пользователя по ID
            var userLocal = _remoteDatabaseContext.Users
                    .Where(x => x.UserId == userId).FirstOrDefault();

            // Если пользователь не найден, возвращаем null
            if (userLocal == null) return null;

            return userLocal;
        }

        public bool UpdateUser(UserDao userUpdate) //Метод для обновления пользователя
        {
            // Находим пользователя по ID для обновления
            var userLocal = _remoteDatabaseContext.Users
                    .Where(x => x.UserId == userUpdate.UserId).FirstOrDefault();

            // Если пользователь не найден, возвращаем false
            if (userLocal == null) return false;

            // Обновляем информацию о пользователе
            userLocal.FIO = userUpdate.FIO;
            userLocal.GroupId = userUpdate.GroupId;

            // Сохраняем изменения
            _remoteDatabaseContext.SaveChanges();
            return true; 
        }

        public bool UpdateUserById(int userId, string fio, int groupId) //Метод для обновления пользователя по его Id
        {
            // Находим пользователя по ID
            var userLocal = _remoteDatabaseContext.Users
                    .Where(x => x.UserId == userId).FirstOrDefault();

            // Если пользователь не найден, возвращаем false
            if (userLocal == null) return false;

            // Обновляем информацию о пользователе
            userLocal.FIO = fio;
            userLocal.GroupId = groupId;

            // Сохраняем изменения
            _remoteDatabaseContext.SaveChanges();
            return true; 
        }

        public bool RemoveUserById(int userId) //Метод для удаления пользователя по его Id
        {
            // Находим пользователя по ID
            var userLocal = _remoteDatabaseContext.Users
                .Where(x => x.UserId == userId).FirstOrDefault();

            // Если пользователь не найден, возвращаем false
            if (userLocal == null) return false;

            // Удаляем пользователя из контекста и сохраняем изменения
            _remoteDatabaseContext.Users.Remove(userLocal);
            _remoteDatabaseContext.SaveChanges();
            return true;
        }

    }
}