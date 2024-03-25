using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class SubscriptionsTemplatesRepository : BaseRepository<SubscriptionTemplate>,
    ISubscriptionsTemplatesRepository
{
    public SubscriptionsTemplatesRepository(AuditDbContext context) : base(context)
    {
    }
}
