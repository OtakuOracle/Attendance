using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using presence.domain.UseCase;
using presence.data.Repository;

namespace presence.ui
{
    public class PresenceConsoleUI
    {
        private readonly PresenceUseCase _presenceUseCase;

        private readonly IPresenceRepository _presenceRepository;
        public PresenceConsoleUI(PresenceUseCase presenceUseCase, IPresenceRepository presenceRepository)
        {
            _presenceUseCase = presenceUseCase;
            _presenceRepository = presenceRepository;
        }

        public void GetPresenceByGroup(string groupId)
        {

            int Id;
            bool isParsed = int.TryParse(groupId, out Id);
            if (!isParsed)
            {
                Console.WriteLine("Введено не число");
            }
            StringBuilder presenceOutput = new StringBuilder();
            var presence = _presenceUseCase.GetPresenceByGroup(Id);
            foreach (var p in presence)
            {
                presenceOutput.AppendLine($"{p.User.Id}\t{p.User.FIO}\t{p.ClassNumber}\t{p.Date}\t{p.IsAttendence}");
            }
            Console.WriteLine(presenceOutput);
        }

        public void GetPresenceByGroupAndDate(string groupId, string date)
        {
            int Id;
            DateOnly Data;
            bool isParsed = int.TryParse(groupId, out Id);
            bool isParsedData = DateOnly.TryParse(date, out Data);
            if (!isParsed && !isParsedData)
            {
                Console.WriteLine("Введено не число в группе ID или введена неправильно дата");
            }
            StringBuilder presenceOutput = new StringBuilder();
            var presence = _presenceUseCase.GetPresenceByGroupAndDate(Id, Data);
            foreach (var p in presence)
            {
                presenceOutput.AppendLine($"{p.User.Id}\t{p.User.FIO}\t{p.ClassNumber}\t{p.Date}\t{p.IsAttendence}");
            }
            Console.WriteLine(presenceOutput);
        }

        public void UncheckAttendence(string firstClass, string lastClass, string date, string userId)
        {
            int fClass;
            int lClass;
            int uId;
            DateOnly Data;
            bool isParsedFClass = int.TryParse(firstClass, out fClass);
            bool isParsedLClass = int.TryParse(lastClass, out lClass);
            bool isParsedUId = int.TryParse(userId, out uId);
            bool isParsedData = DateOnly.TryParse(date, out Data);
            if (!isParsedFClass && !isParsedData && !isParsedLClass && !isParsedUId)
            {
                Console.WriteLine("Введен не числа для одних из этих значений: первый урок, последний урок, ID юзера или неправильно введена датаю");
            }
            string output = _presenceUseCase.UncheckAttendence(fClass, lClass, Data, uId) ?
                "Посещаемость обновлена" : "Посещаемость не обновлена";
            Console.WriteLine(output);
        }

        public void AddPresence(string firstClass, string lastClass, string groupId, string date)
        {
            int fClass;
            int lClass;
            int gId;
            DateOnly data;
            bool isParsedFClass = int.TryParse(firstClass, out fClass);
            bool isParsedLClass = int.TryParse(lastClass, out lClass);
            bool isParsedGId = int.TryParse(groupId, out gId);
            bool isParsedData = DateOnly.TryParse(date, out data);
            if (!isParsedFClass && !isParsedData && !isParsedLClass && !isParsedGId)
            {
                Console.WriteLine("Введен не числа для одних из этих значений: первый урок, последний урок, ID группы или неправильно введена датаю");
            }
            _presenceUseCase.AddPresence(fClass, lClass, gId, data);
            Console.WriteLine("Посещаемость добавлена");
        }

        public void GetPresenceStatsByGroup(string groupId)
        {

            int Id;
            bool isParsed = int.TryParse(groupId, out Id);
            if (!isParsed)
            {
                Console.WriteLine("Введено не число");
            }

            var stats = _presenceUseCase.GetPresenceStatsByGroup(Id);
            StringBuilder output = new StringBuilder();

            output.AppendLine($"Информация о группе {groupId}:");
            output.AppendLine($"Количество студентов: {stats["Количество студентов"]}");
            output.AppendLine($"Количество занятий: {stats["Количество занятий"]}");
            output.AppendLine($"Общий процент посещаемости: {stats["Процент посещаемости"]}%");
            output.AppendLine("\nСтатистика по студентам:");

            var presence = _presenceUseCase.GetPresenceByGroup(Id);
            var students = presence.GroupBy(p => p.User)
                                 .Select(g => new
                                 {
                                     Student = g.Key,
                                     Total = stats["Количество занятий"],
                                     Attended = g.Count(p => p.IsAttendence),
                                     Missed = stats["Количество занятий"] - g.Count(p => p.IsAttendence),
                                     Percentage = (g.Count(p => p.IsAttendence) * 100) / stats["Количество занятий"]
                                 });

            foreach (var student in students)
            {
                output.AppendLine($"\nСтудент: {student.Student.FIO}");
                output.AppendLine($"Посещено занятий: {student.Attended}");
                output.AppendLine($"Пропущено занятий: {student.Missed}");
                output.AppendLine($"Процент посещаемости: {student.Percentage}%");
            }

            Console.WriteLine(output.ToString());
        }

        public void GenerateWeeklyPresence(string firstClass, string lastClass, string groupId, string date)
        {
            int fClass;
            int lClass;
            int gId;
            DateOnly data;
            bool isParsedFClass = int.TryParse(firstClass, out fClass);
            bool isParsedLClass = int.TryParse(lastClass, out lClass);
            bool isParsedGId = int.TryParse(groupId, out gId);
            bool isParsedData = DateOnly.TryParse(date, out data);
            if (!isParsedFClass && !isParsedData && !isParsedLClass && !isParsedGId)
            {
                Console.WriteLine("Введен не числа для одних из этих значений: первый урок, последний урок, ID группы или неправильно введена дата");
            }
            _presenceUseCase.GenerateWeeklyPresence(fClass, lClass, gId, data);
            Console.WriteLine("Посещаемость на неделю сгенерирована");
        }
    }
}