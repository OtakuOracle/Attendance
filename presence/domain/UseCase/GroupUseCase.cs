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
    public class GroupUseCase
    {
        private readonly IGroupRepository _repositoryGroupImpl;

        public GroupUseCase(IGroupRepository repositoryGroupImpl)
        {
            _repositoryGroupImpl = repositoryGroupImpl;
        }

        public List<GroupResponse> GetAllGroups() => _repositoryGroupImpl.GetAllGroup() // Метод для получения списка всех групп
            .Select(it => new GroupResponse { Id = it.Id, Name = it.Name }).ToList();

        public bool UpdateGroupName(String id, String name1) //Метод для обновления названия группы 
        {
            return _repositoryGroupImpl.UpdateGroupById(int.Parse(id), name1);
        }
        
        public bool AddGroup(String name, int id) //Метод для добавления новой группы
        {
            return _repositoryGroupImpl.AddGroup(new GroupDao { Name = name, Id = id });
        }
    }
}