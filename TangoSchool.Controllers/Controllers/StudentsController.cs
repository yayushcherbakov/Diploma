using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Models.Teachers;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("Headers")]
    public async Task<ActionResult<List<TeacherHeader>>> GetStudentHeaders
    (
        CancellationToken cancellationToken
    )
    {
        return Ok(await _studentService.GetStudentHeaders(cancellationToken));
    }
}
