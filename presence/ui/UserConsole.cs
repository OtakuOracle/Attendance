using presence.data.LocalData.Entity;
using presence.domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.ui
{
    public class UserConsoleUI
    {
        UserUseCase _userUseCase;
        public UserConsoleUI(UserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        public void DisplayAllUsers()
        {
            StringBuilder userOutput = new StringBuilder();
            foreach (var user in _userUseCase.GetAllUsers())
            {
                userOutput.AppendLine($"{user.Id}\t{user.FIO}\t{user.GroupId}");
            }
            Console.WriteLine(userOutput);
        }


        public void RemoveUserById(string userId)
        {

            int Id;
            bool isParsed = int.TryParse(userId, out Id);
            if (!isParsed)
            {
                Console.WriteLine("Введено не число");
            }
            string output = _userUseCase.RemoveUserById(Id) ? "Пользователь удален" : "Пользователь не удален";
            Console.WriteLine(output);
        }

        public void UpdateUserById(string userId, String name, string groupId)
        {
            int UserId;
            int GroupId;
            bool isParsed = int.TryParse(userId, out UserId);
            bool isParsedGroup = int.TryParse(groupId, out GroupId);
            if (!isParsed && !isParsedGroup)
            {
                Console.WriteLine("Введено неверное число");
            }
            string output = _userUseCase.UpdateUserById(UserId, name, GroupId) ? "Пользователь обновлен" : "Пользователь не обновлен";
            Console.WriteLine(output);
        }

        public void GetUserById(string userId)
        {
            int Id;
            bool isParsed = int.TryParse(userId, out Id);
            if (!isParsed)
            {
                Console.WriteLine("Введено не число");
            }
            StringBuilder userOutput = new StringBuilder();
            var user = _userUseCase.GetUserById(Id);
            userOutput.AppendLine($"{user.Id}\t{user.FIO}\t{user.GroupId}");
            Console.WriteLine(userOutput);
        }

    }
}


