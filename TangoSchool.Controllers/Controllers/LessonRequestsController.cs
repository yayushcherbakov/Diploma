using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.LessonRequests;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(Roles = RoleConstants.Student, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("LessonRequest")]
public class LessonRequestsController : ControllerBase
{
    private readonly ILessonRequestsService _lessonRequestsService;

    public LessonRequestsController(ILessonRequestsService lessonRequestsService)
    {
        _lessonRequestsService = lessonRequestsService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateLessonRequest
    (
        [FromBody] CreateLessonRequestPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonRequestsService.CreateLessonRequest(payload, cancellationToken));
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateLessonRequest
    (
        [FromBody] UpdateLessonRequest payload,
        CancellationToken cancellationToken
    )
    {
        await _lessonRequestsService.UpdateLessonRequest(payload, cancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetLessonRequestResponse>> GetLessonRequest
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonRequestsService.GetLessonRequest(id, cancellationToken));
    }
}
