using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Lessons;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.Extensions;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Lessons")]
public class LessonsController : ControllerBase
{
    private readonly ILessonsService _lessonsService;

    public LessonsController(ILessonsService lessonsService)
    {
        _lessonsService = lessonsService;
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Administrator}")]
    [HttpPost("Metadata")]
    public async Task<ActionResult<LessonsMetadata>> GetLessonsMetadata
    (
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonsService.GetLessonsMetadata(cancellationToken));
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Administrator}")]
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateLesson
    (
        [FromBody] CreateLessonPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonsService.CreateLesson(payload, cancellationToken));
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Administrator}")]
    [HttpPut("Update")]
    public async Task<ActionResult> UpdateLesson
    (
        [FromBody] UpdateLesson payload,
        CancellationToken cancellationToken
    )
    {
        await _lessonsService.UpdateLesson(payload, cancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetLessonResponse>> GetLesson
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonsService.GetLesson(id, cancellationToken));
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    [HttpGet("All")]
    public async Task<ActionResult<GetAllLessonsResponse>> GetAllLessons
    (
        [FromQuery] GetAllLessonsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonsService.GetAllLessons(payload, cancellationToken));
    }

    [Authorize(Roles = RoleConstants.Student)]
    [HttpGet("AllByStudent")]
    public async Task<ActionResult<GetAllLessonsResponse>> GetAllLessonsByStudent
    (
        [FromQuery] GetAllLessonsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonsService.GetAllLessonsByStudent(
            User.GetUserId(), payload, cancellationToken));
    }

    [Authorize(Roles = RoleConstants.Teacher)]
    [HttpGet("AllByTeacher")]
    public async Task<ActionResult<GetAllLessonsResponse>> GetAllLessonsByTeacher
    (
        [FromQuery] GetAllLessonsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonsService.GetAllLessonsByTeacher(
            User.GetUserId(), payload, cancellationToken));
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Administrator}")]
    [HttpPost("{id:guid}/SetLessonAttendance")]
    public async Task<ActionResult> SetLessonAttendance
    (
        [FromRoute] Guid id,
        [FromBody] SetLessonAttendancePayload payload,
        CancellationToken cancellationToken
    )
    {
        await _lessonsService.SetLessonAttendance(id, payload, cancellationToken);

        return Ok();
    }
    
    [HttpPost("{id:guid}/Terminate")]
    public async Task<ActionResult> TerminateLesson
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _lessonsService.TerminateLesson(id, cancellationToken);

        return Ok();
    }

    [HttpPost("{id:guid}/Restore")]
    public async Task<ActionResult> RestoreLesson
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _lessonsService.RestoreLesson(id, cancellationToken);

        return Ok();
    }
}
