using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class GroupsRepository : BaseRepository<Group>, IGroupsRepository
{
    public GroupsRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
