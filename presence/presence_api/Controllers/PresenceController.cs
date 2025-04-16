using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using presence.domain.Models;
using presence.domain.UseCase;

namespace presence_api.Controllers.PresenceController;

[ApiController]
[Route("api/[controller]")]
public class PresenceController : ControllerBase
{
    private readonly PresenceUseCase _presenceUseCase;

    public PresenceController(PresenceUseCase presenceUseCase)
    {
        _presenceUseCase = presenceUseCase;
    }

    [HttpPost("~/IsAttendence")] //Добавление информации о посещаемости
    public ActionResult<PresenceResponse> UpdateAttendance([FromQuery] int FirstClass,
           [FromQuery] int LastClass, [FromQuery] string Data,
           [FromQuery] int UserId)
    {
        DateOnly data;
        bool isParsed = DateOnly.TryParse(Data, out data);
        if (!isParsed)
        {
            return BadRequest("Неверный формат");
        }
        return Ok(_presenceUseCase.UncheckAttendence(FirstClass, LastClass, data, UserId));
    }

    [HttpPost("~/AddPresence")]  //Добавление записи о присутствии
    public ActionResult<IEnumerable<Presence>> PostPresence([FromQuery] int GroupId,
    [FromQuery] string StartData, [FromQuery] string EndData, [FromQuery] int UserId)
    {
        return Ok(_presenceUseCase.AddPresenceByDate(StartData, EndData, GroupId));
    }

    [HttpGet] //Получение информации о присутствии
    public ActionResult<IEnumerable<Presence>> GetPresence(
    [FromQuery] int groupId,
    [FromQuery] string startDataString,
    [FromQuery] string endDataString,
    [FromQuery] int userId)
    {
        // Объявление переменных для хранения преобразованных дат
        DateOnly startData;
        DateOnly endData;

        // Попытка преобразования строковых дат в DateOnly
        bool isStartParsed = DateOnly.TryParse(startDataString, out startData);
        bool isEndParsed = DateOnly.TryParse(endDataString, out endData);

        // Проверка на корректность формата даты начала
        if (!isStartParsed)
        {
            return BadRequest("Неверный формат даты начала");
        }

        // Проверка на корректность формата даты конца
        if (!isEndParsed)
        {
            return BadRequest("Неверный формат даты конца");
        }

        // Возвращает список присутствия 
        var presenceList = _presenceUseCase.GetPresence(groupId, startData, endData, userId);
        return Ok(presenceList);
    }

    [HttpDelete("~/DeleteByUser")] //Удаление записи о присутствии по пользователю
    public ActionResult<IEnumerable<PresenceResponse>> DeletePresenceByUser([FromQuery] int UserId)
    {
        return Ok(_presenceUseCase.DeletePresenceByUser(UserId));
    }

    [HttpDelete("~/DeleteByGroup")]//Удаление записи о присутствии по группе
    public ActionResult<IEnumerable<PresenceResponse>> DeletePresenceByGroup([FromQuery] int GroupId)
    {
        return Ok(_presenceUseCase.DeletePresenceByGroup(GroupId));
    }

    [HttpDelete("~/DeleteByDate")] //Удаление записей о присутствии по дате
    public ActionResult<IEnumerable<PresenceResponse>> DeletePresenceByDate(
        [FromQuery] string StartData,
        [FromQuery] string EndData)
    {
        DateOnly startData;
        DateOnly endData;

        // Попытка преобразования строковых дат в DateOnly
        bool isParsed = DateOnly.TryParse(StartData, out startData);
        bool isParse = DateOnly.TryParse(EndData, out endData);

        // Проверка на корректность формата даты начала
        if (!isParsed)
        {
            return BadRequest("Неверный формат даты начала.");
        }
        // Проверка на корректность формата даты конца
        if (!isParse)
        {
            return BadRequest("Неверный формат даты конца.");
        }

        // Возвращает результат удаления
        return Ok(_presenceUseCase.DeletePresenceByDate(startData, endData));
    }

}
