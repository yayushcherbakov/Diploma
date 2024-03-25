using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class AuditLogsRepository : BaseRepository<AuditLog>, IAuditLogsRepository
{
    public AuditLogsRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
