using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Classrooms;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(Roles = RoleConstants.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Classrooms")]
public class ClassroomsController : ControllerBase
{
    private readonly IClassroomsService _classroomsService;

    public ClassroomsController(IClassroomsService classroomsService)
    {
        _classroomsService = classroomsService;
    }
    
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateClassroom
    (
        [FromBody] CreateClassroomPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _classroomsService.CreateClassroom(payload, cancellationToken));
    }
    
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
}
