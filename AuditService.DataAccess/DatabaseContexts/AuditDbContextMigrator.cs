using AuditService.DataAccess.DatabaseContexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuditService.DataAccess.DatabaseContexts;

internal class AuditDbContextMigrator : IAuditDbContextMigrator
{
    private readonly AuditDbContext _auditDbContext;

    public AuditDbContextMigrator(AuditDbContext auditDbContext)
    {
        _auditDbContext = auditDbContext;
    }

    public async Task Migrate(CancellationToken cancellationToken)
    {
        await _auditDbContext.Database.MigrateAsync(cancellationToken);
    }
}
