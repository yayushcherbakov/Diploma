using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Lessons;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(Roles = RoleConstants.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Lessons")]
public class LessonsController : ControllerBase
{
    private readonly ILessonsService _lessonsService;
    
    public LessonsController(ILessonsService lessonsService)
    {
        _lessonsService = lessonsService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateLesson
    (
        [FromBody] CreateLessonPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonsService.CreateLesson(payload, cancellationToken));
    }

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
}
