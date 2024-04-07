using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.LessonRequests;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.Extensions;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("LessonRequest")]
public class LessonRequestsController : ControllerBase
{
    private readonly ILessonRequestsService _lessonRequestsService;

    public LessonRequestsController(ILessonRequestsService lessonRequestsService)
    {
        _lessonRequestsService = lessonRequestsService;
    }

    [Authorize(Roles = RoleConstants.Student)]
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateLessonRequest
    (
        [FromBody] CreateLessonRequestPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonRequestsService.CreateLessonRequest(User.GetUserId(), payload, cancellationToken));
    }

    [Authorize(Roles = $"{RoleConstants.Teacher},{RoleConstants.Student}")]
    [HttpPost("{id:guid}/Reject")]
    public async Task<ActionResult> RejectLessonRequest
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _lessonRequestsService.RejectLessonRequest(User.GetUserId(), id, cancellationToken);

        return Ok();
    }

    [Authorize(Roles = RoleConstants.Teacher)]
    [HttpPost("{id:guid}/Approve")]
    public async Task<ActionResult> RejectLessonRequest
    (
        [FromRoute] Guid id,
        [FromBody] ApproveLessonRequestPayload payload,
        CancellationToken cancellationToken
    )
    {
        await _lessonRequestsService.ApproveLessonRequest(User.GetUserId(), id, payload, cancellationToken);

        return Ok();
    }

    [Authorize(Roles = RoleConstants.Teacher)]
    [HttpGet("AllByTeacher")]
    public async Task<ActionResult<GetLessonRequestByTeacherResponse>> GetLessonRequestsByTeacher
    (
        [FromQuery] GetLessonRequestByTeacherPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonRequestsService.GetLessonRequestsByTeacher(
            User.GetUserId(), payload, cancellationToken));
    }

    [Authorize(Roles = RoleConstants.Student)]
    [HttpGet("AllByStudent")]
    public async Task<ActionResult<GetLessonRequestByTeacherResponse>> GetLessonRequestsByStudent
    (
        [FromQuery] GetLessonRequestByTeacherPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _lessonRequestsService.GetLessonRequestsByStudent(
            User.GetUserId(), payload, cancellationToken));
    }
}
