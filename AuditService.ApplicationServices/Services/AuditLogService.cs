using AuditService.ApplicationServices.Models.AuditLogs;
using AuditService.ApplicationServices.Services.Interfaces;
using AuditService.DataAccess.DatabaseContexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuditService.ApplicationServices.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IReadOnlyAuditDbContext _readOnlyAuditDbContext;

    public AuditLogService(IReadOnlyAuditDbContext readOnlyAuditDbContext)
    {
        _readOnlyAuditDbContext = readOnlyAuditDbContext;
    }

    public async Task<GetAuditLogsResponse> GetAuditLogs
    (
        GetAuditLogsPayload payload,
        CancellationToken cancellationToken
    )
    {
        var query = _readOnlyAuditDbContext.AuditLogs.AsQueryable();

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .OrderBy(x => x.Timestamp)
            .Skip(payload.Page * payload.ItemsPerPage)
            .Take(payload.ItemsPerPage)
            .Select(x => new AuditLogModel
            (
                x.ApplicationUserId,
                x.ApplicationUser.FirstName,
                x.Timestamp,
                x.AuditLogType
            ))
            .ToListAsync(cancellationToken);

        return new(result, totalCount);
    }

    public async Task<GetAuditLogsResponse> GetAuditLogsByIdentityId
    (
        GetAuditLogsByIdentityIdPayload payload,
        CancellationToken cancellationToken
    )
    {
        var query = _readOnlyAuditDbContext.AuditLogs
            .Where(x => x.ApplicationUserId == payload.UserId);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .OrderBy(x => x.Timestamp)
            .Skip(payload.Page * payload.ItemsPerPage)
            .Take(payload.ItemsPerPage)
            .Select(x =>
                new AuditLogModel
                (
                    x.ApplicationUserId,
                    x.ApplicationUser.FirstName,
                    x.Timestamp,
                    x.AuditLogType
                ))
            .ToListAsync(cancellationToken);

        return new(result, totalCount);
    }
}
