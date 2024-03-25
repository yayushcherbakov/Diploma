using AuditService.ApplicationServices.Constants;
using AuditService.ApplicationServices.Models.AuditLogs;
using AuditService.ApplicationServices.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Controllers;

[ApiController]
[Authorize(Roles = RoleConstants.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("AuditLogs")]
public class AuditLogsController : ControllerBase
{
    private readonly IAuditLogService _logService;

    public AuditLogsController(IAuditLogService logService)
    {
        _logService = logService;
    }

    [HttpGet("GetAuditLogs")]
    public async Task<ActionResult<GetAuditLogsResponse>> GetAuditLogs
    (
        [FromQuery] GetAuditLogsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return await _logService.GetAuditLogs(payload, cancellationToken);
    }

    [HttpGet("GetAuditLogsByUserId")]
    public async Task<ActionResult<GetAuditLogsResponse>> GetAuditLogsByIdentityId
    (
        [FromQuery] GetAuditLogsByIdentityIdPayload payload,
        CancellationToken cancellationToken
    )
    {
        return await _logService.GetAuditLogsByIdentityId(payload, cancellationToken);
    }
}
