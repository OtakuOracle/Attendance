using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Models.ResponseModels;
using domain.UseCase;
using Microsoft.AspNetCore.Mvc;
using presence.domain.Models;
using presence.domain.UseCase;

namespace presence_api.Controllers.UserController;

[ApiController]
[Route("api/[admin]")]
public class AdminController : ControllerBase
{
    private readonly AdminUseCase _adminUseCase;

    public AdminController(AdminUseCase adminUseCase)
    {
        _adminUseCase = adminUseCase;
    }

    [HttpPost("~/AddStudent")] //Добавление студента
    public ActionResult<String> AddStudent([FromQuery] string GroupName, [FromQuery] List<string> students)
    {
        return _adminUseCase.AddStudents(GroupName, students) ? "Студент добавлен" : "Студент не добавлен";
    }

    [HttpGet("~/GetStudentInfo")] //Получение инф-ции о студенте
    public ActionResult<UserResponse?> GetStudentInfo([FromQuery] int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Неправельный Id");
        }

        var studentInfo = _adminUseCase.GetStudentInfo(userId);
        if (studentInfo == null)
        {
            return NotFound("Студент не найден");
        }

        return Ok(studentInfo);
    }

    [HttpGet("~/GetAllGroupsWithStudents")] //Вывести все группы со студентами
    public ActionResult<IEnumerable<GroupResponse>> GetAllGroupsWithStudents()
    {
        return Ok(_adminUseCase.GetAllGroupsWithStudents());
    }

    [HttpDelete("~/DeleteUserId")] //Удалить Id пользователя
    public ActionResult<string> DeleteUserId([FromQuery] int userId, [FromQuery] int groupId)
    {
        return _adminUseCase.DeleteUserFromGroup(userId, groupId) ? "Пользователь удален" : "Пользователь не удален";
    }

    [HttpDelete("~/DeleteGroupId")] //Удалить Id  группы 
    public ActionResult<String> DeleteGroupId([FromQuery] int groupId)
    {
        return _adminUseCase.DeleteGroup(groupId) ? "Группа удалена" : "Группа не удалена";
    }
}