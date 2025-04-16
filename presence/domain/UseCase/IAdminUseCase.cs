using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Models.ResponseModels;

namespace domain.UseCase
{
    public interface IAdminUseCase
    {
        IEnumerable<GroupResponse> GetAllGroupsWithStudents();
        UserResponse GetStudentInfo(int userId);
        bool AddStudents(string GroupName, List<string> Students);
        bool DeleteGroup(int groupId);
        bool DeleteUserFromGroup(int userId, int groupId);
    }
}