using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Models.ResponseModels;
using domain.Request;
using presence.domain.Models;

namespace domain.UseCase
{
    public interface IGroupUseCase
    {
        public Task<IEnumerable<GroupResponse>> GetGroupsWithStudentsAsync();
        public IEnumerable<GroupResponse> GetGroupsWithStudents();
        public void AddGroup(AddGroupRequest addGroupRequest);
        public void AddGroupWithStudents(AddGroupWithStudentsRequest addGroupWithStudents);
        void RemoveUserFromGroup(int UserId);

    }
}