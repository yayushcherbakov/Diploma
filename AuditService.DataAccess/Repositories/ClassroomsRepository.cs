using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class ClassroomsRepository : BaseRepository<Classroom>, IClassroomsRepository
{
    protected ClassroomsRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
