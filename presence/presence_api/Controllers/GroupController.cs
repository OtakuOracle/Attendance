using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using presence.domain.Models;
using presence.domain.UseCase;


namespace presence.presence_api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class GroupController : ControllerBase
{
    private readonly GroupUseCase _groupUseCase;
    public GroupController(GroupUseCase groupUseCase)
    {
        _groupUseCase = groupUseCase;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Group>> GetAllGroups() //Метод контроллера для получения всех групп 
    {
        return Ok(_groupUseCase.GetAllGroups());
    }
}