using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Models.Teachers;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.Extensions;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Teachers")]
public class TeachersController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeachersController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet("Headers")]
    public async Task<ActionResult<List<TeacherHeader>>> GetTeacherHeaders
    (
        CancellationToken cancellationToken
    )
    {
        return Ok(await _teacherService.GetTeacherHeaders(cancellationToken));
    }

    [HttpGet("Current/Groups")]
    public async Task<ActionResult<GetCurrentTeacherGroupsResponse>> GetCurrentTeacherGroups
    (
        GetCurrentTeacherGroupsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _teacherService.GetCurrentTeacherGroups(User.GetUserId(), payload, cancellationToken));
    }
}
