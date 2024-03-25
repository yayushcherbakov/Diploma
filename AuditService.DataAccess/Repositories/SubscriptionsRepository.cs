using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class SubscriptionsRepository : BaseRepository<Subscription>, ISubscriptionsRepository
{
    protected SubscriptionsRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
