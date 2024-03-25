using AuditService.ApplicationServices.Constants;
using AuditService.ApplicationServices.Models.Identities;
using AuditService.ApplicationServices.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Controllers;

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

    [HttpGet("GetUserInformation/{id:guid}")]
    public async Task<ActionResult<UserInformationWithRoles>> GetUserInformation
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.GetUserInformation(id, cancellationToken);
    }

    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<GetAllUsersResponse>> GetAllUsers
    (
        [FromQuery] GetAllUsersPayload payload,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.GetAllUsers(payload, cancellationToken);
    }


    [HttpPost("RegisterTeacher")]
    public async Task<ActionResult<AuthResponse>> RegisterTeacher
    (
        [FromBody] RegisterTeacherPayload userPayload,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.RegisterTeacher(userPayload, cancellationToken);
    }

    [HttpPost("RegisterStudent")]
    public async Task<ActionResult<AuthResponse>> RegisterStudent
    (
        [FromBody] RegisterStudentPayload userPayload,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.RegisterStudent(userPayload, cancellationToken);
    }

    [HttpPost("RegisterAdministrator")]
    public async Task<ActionResult<AuthResponse>> RegisterAdministrator
    (
        [FromBody] RegisterAdministratorPayload userPayload,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.RegisterAdministrator(userPayload, cancellationToken);
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
