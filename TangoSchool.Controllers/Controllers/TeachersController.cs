using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Classrooms;
using TangoSchool.ApplicationServices.Models.Teachers;
using TangoSchool.ApplicationServices.Services.Interfaces;

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
}
