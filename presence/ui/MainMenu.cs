using presence.data.Repository;
using presence.domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.ui
{
    public class MainMenuUI
    {

        UserConsoleUI _userConsoleUI;
        GroupConsoleUI _groupConsoleUI;
        PresenceConsoleUI _presenceConsoleUI;

        public MainMenuUI(UserUseCase userUseCase, GroupUseCase groupUseCase, PresenceUseCase presenceUseCase, IPresenceRepository presenceRepository)
        {
            _userConsoleUI = new UserConsoleUI(userUseCase);
            _groupConsoleUI = new GroupConsoleUI(groupUseCase);
            _presenceConsoleUI = new PresenceConsoleUI(presenceUseCase, presenceRepository);


            DisplayMenu();

        }

        public void DisplayMenu() //Вывод консольного меню
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Показать всех пользователей");
                Console.WriteLine("2 - Удалить пользователя по ID");
                Console.WriteLine("3 - Показать все группы");
                Console.WriteLine("4 - Найти пользователя по ID");
                Console.WriteLine("5 - Обновить данные пользователя по ID");
                Console.WriteLine("6 - Обновить название группы");
                Console.WriteLine("7 - Добавить новую группу");
                Console.WriteLine("8 - Показать посещаемость группы");
                Console.WriteLine("9 - Показать посещаемость группы за определенную дату");
                Console.WriteLine("10 - Отменить отметку о присутствии");
                Console.WriteLine("11 - Добавить отметку о присутствии");
                Console.WriteLine("13 - Сгенерировать недельную посещаемость");
                Console.WriteLine("14 - Получить статистику посещаемости по группе");

                switch (Console.ReadLine())
                {
                    case "1": _userConsoleUI.DisplayAllUsers(); break;
                    case "2": _userConsoleUI.RemoveUserById(Console.ReadLine()); break;
                    case "3": _userConsoleUI.GetUserById(Console.ReadLine()); break;
                    case "4": _userConsoleUI.UpdateUserById(Console.ReadLine(), Console.ReadLine(), Console.ReadLine()); break;

                    case "5": _groupConsoleUI.DisplayAllGroups(); break;
                    case "6": _groupConsoleUI.UpdateGroupName(Console.ReadLine(), Console.ReadLine()); break;
                    case "7": _groupConsoleUI.AddGroup(Console.ReadLine(), Console.ReadLine()); break;

                    case "8": _presenceConsoleUI.GetPresenceByGroup(Console.ReadLine()); break;
                    case "9": _presenceConsoleUI.GetPresenceByGroupAndDate(Console.ReadLine(), Console.ReadLine()); break;
                    case "10": _presenceConsoleUI.UncheckAttendence(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine()); break;
                    case "11": _presenceConsoleUI.AddPresence(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine()); break;
                    case "13": _presenceConsoleUI.GenerateWeeklyPresence(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine()); break;
                    case "14": _presenceConsoleUI.GetPresenceStatsByGroup(Console.ReadLine()); break;

                    default:
                        DisplayMenu();
                        break;
                }

            }
        }

    }
}
