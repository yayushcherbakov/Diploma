using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Classrooms;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Classrooms")]
public class ClassroomsController : ControllerBase
{
    private readonly IClassroomsService _classroomsService;

    public ClassroomsController(IClassroomsService classroomsService)
    {
        _classroomsService = classroomsService;
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateClassroom
    (
        [FromBody] CreateClassroomPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _classroomsService.CreateClassroom(payload, cancellationToken));
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    [HttpPut("Update")]
    public async Task<ActionResult> UpdateClassroom
    (
        [FromBody] UpdateClassroom payload,
        CancellationToken cancellationToken
    )
    {
        await _classroomsService.UpdateClassroom(payload, cancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetClassroomResponse>> GetClassroom
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _classroomsService.GetClassroom(id, cancellationToken));
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    [HttpPost("{id:guid}/Terminate")]
    public async Task<ActionResult> TerminateClassroom
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _classroomsService.TerminateClassroom(id, cancellationToken);

        return Ok();
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    [HttpPost("{id:guid}/Restore")]
    public async Task<ActionResult> RestoreClassroom
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _classroomsService.RestoreClassroom(id, cancellationToken);

        return Ok();
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Administrator}")]
    [HttpGet("All")]
    public async Task<ActionResult<GetAllClassroomsResponse>> GetAllClassrooms
    (
        [FromQuery] GetAllClassroomsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _classroomsService.GetAllClassrooms(payload, cancellationToken));
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Administrator}")]
    [HttpGet("AvailableClassrooms")]
    public async Task<ActionResult<List<ClassroomHeader>>> GetAvailableClassrooms
    (
        [FromQuery] GetAvailableClassroomsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _classroomsService.GetAvailableClassrooms(payload, cancellationToken));
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Administrator}")]
    [HttpGet("Headers")]
    public async Task<ActionResult<List<ClassroomHeader>>> GetClassroomHeaders
    (
        CancellationToken cancellationToken
    )
    {
        return Ok(await _classroomsService.GetClassroomHeaders(cancellationToken));
    }
}
