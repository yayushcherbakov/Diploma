using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.AuditLogs;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

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
