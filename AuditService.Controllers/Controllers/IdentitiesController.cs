using AuditService.ApplicationServices.Models.Identities;
using AuditService.ApplicationServices.Services.Interfaces;
using AuditService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Controllers;

[ApiController]
[Route("Identities")]
public class IdentitiesController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentitiesController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate
    (
        [FromBody] AuthRequest request,
        CancellationToken cancellationToken
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return await _identityService.Authenticate(request, cancellationToken);
    }

    [Authorize]
    [HttpGet("GetUserInformation")]
    public async Task<ActionResult<UserInformationWithRoles>> GetUserInformation
    (
        CancellationToken cancellationToken
    )
    {
        return await _identityService.GetUserInformation(User.GetUserId(), cancellationToken);
    }

    [Authorize]
    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser
    (
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken
    )
    {
        if (!ModelState.IsValid) return BadRequest(request);

        await _identityService.UpdateUser(User.GetUserId(), request, cancellationToken);

        return Ok();
    }

    [Authorize]
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword
    (
        [FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken
    )
    {
        if (!ModelState.IsValid) return BadRequest(request);

        await _identityService.ChangePassword(User.GetUserId(), request, cancellationToken);

        return Ok();
    }

    [Authorize]
    [HttpPost]
    [Route("RevokeToken")]
    public async Task<IActionResult> RevokeToken
    (
        CancellationToken cancellationToken
    )
    {
        await _identityService.RevokeToken(User.GetUserId(), cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Route("RefreshToken")]
    public async Task<ActionResult<TokenModel>> RefreshToken
    (
        TokenModel tokenModel,
        CancellationToken cancellationToken
    )
    {
        return await _identityService.RefreshToken(tokenModel, cancellationToken);
    }

    [HttpPost]
    [Route("ResetPassword/Request")]
    public async Task<ActionResult> RequestResetPassword
    (
        RequestResetPasswordPayload payload,
        CancellationToken cancellationToken
    )
    {
        await _identityService.RequestResetPassword(payload, cancellationToken);

        return Ok();
    }

    [HttpPost]
    [Route("ResetPassword/Confirm")]
    public async Task<ActionResult<TokenModel>> ConfirmResetPassword
    (
        ConfirmResetPasswordPayload payload,
        CancellationToken cancellationToken
    )
    {
        await _identityService.ConfirmResetPassword(payload, cancellationToken);

        return Ok();
    }
}
