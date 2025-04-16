using presence.domain.Models;
using presence.domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presence.ui
{
    public class GroupConsoleUI
    {
        GroupUseCase _groupUseCase;
        public GroupConsoleUI(GroupUseCase groupUseCase)
        {
            _groupUseCase = groupUseCase;
        }

        public void AddGroup(String name, String id) //Метод для добавления новой группы
        {
            int Id;
            bool isParsed = int.TryParse(id, out Id);
            if (!isParsed)
            {
                Console.WriteLine("Введено не число");
            }
            string output = _groupUseCase.AddGroup(name, Id) ? "Группа добавлена" : "Группа не добавлена";
            Console.WriteLine(output);
        }

        public void UpdateGroupName(String name, String name1) //Метод для обновления имени группы 
        {
            string output = _groupUseCase.UpdateGroupName(name, name1) ? "Группа обновлена" : "Группа не обновлена";
            Console.WriteLine(output);
        }

        public void DisplayAllGroups() //Метод для вывода всех существующих групп
        {
            StringBuilder groupOutput = new StringBuilder();
            foreach (var group in _groupUseCase.GetAllGroups())
            {
                groupOutput.AppendLine($"{group.Id}\t{group.Name}");
            }
            Console.WriteLine(groupOutput);
        }
    }
}
