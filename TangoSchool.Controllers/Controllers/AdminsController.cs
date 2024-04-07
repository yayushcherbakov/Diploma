using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Identities;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Route("Admins")]
[Authorize(Roles = RoleConstants.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AdminsController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public AdminsController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpGet("UserInformation/{id:guid}")]
    public async Task<ActionResult<UserInformationWithRoles>> GetUserInformation
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.GetUserInformation(id, cancellationToken);
    }

    [HttpGet("AllUsers")]
    public async Task<ActionResult<GetAllUsersResponse>> GetAllUsers
    (
        [FromQuery] GetAllUsersPayload payload,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.GetAllUsers(payload, cancellationToken);
    }


    [HttpPost("RegisterTeacher")]
    public async Task<ActionResult> RegisterTeacher
    (
        [FromBody] RegisterTeacherPayload userPayload,
        CancellationToken cancellationToken
    )
    {
        await _identityService.RegisterTeacher(userPayload, cancellationToken);

        return Ok();
    }

    [HttpPost("RegisterStudent")]
    public async Task<ActionResult<AuthResponse>> RegisterStudent
    (
        [FromBody] RegisterStudentPayload userPayload,
        CancellationToken cancellationToken
    )
    {
        await _identityService.RegisterStudent(userPayload, cancellationToken);
        
        return Ok();
    }

    [HttpPost("RegisterAdministrator")]
    public async Task<ActionResult<AuthResponse>> RegisterAdministrator
    (
        [FromBody] RegisterAdministratorPayload userPayload,
        CancellationToken cancellationToken
    )
    {
        await _identityService.RegisterAdministrator(userPayload, cancellationToken);
        
        return Ok();
    }

    [HttpPost("Delete/{id:guid}")]
    public async Task<IActionResult> Delete
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _identityService.Delete(id, cancellationToken);

        return Ok();
    }

    [HttpPost("UpdateUser/{id:guid}")]
    public async Task<IActionResult> UpdateUser
    (
        [FromBody] UpdateUserRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _identityService.UpdateUser(id, request, cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Route("RevokeAllTokens")]
    public async Task<IActionResult> RevokeAll
    (
        CancellationToken cancellationToken
    )
    {
        await _identityService.RevokeAll(cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Route("RevokeToken/{userId:guid}")]
    public async Task<IActionResult> RevokeToken
    (
        [FromRoute] Guid userId,
        CancellationToken cancellationToken
    )
    {
        await _identityService.RevokeToken(userId, cancellationToken);

        return Ok();
    }
}
