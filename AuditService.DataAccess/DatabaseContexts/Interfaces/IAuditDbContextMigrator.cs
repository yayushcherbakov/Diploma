namespace AuditService.DataAccess.DatabaseContexts.Interfaces;

public interface IAuditDbContextMigrator
{
    Task Migrate(CancellationToken cancellationToken);
}
