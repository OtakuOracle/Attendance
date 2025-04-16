using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Models.ResponseModels;
using presence.domain.Models;

namespace domain.UseCase
{
    public interface IUserUseCase
    {
        public IEnumerable<UserResponse> GetStudents();


    }
}