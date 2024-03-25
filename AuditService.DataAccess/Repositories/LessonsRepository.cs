using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class LessonsRepository : BaseRepository<Lesson>, ILessonsRepository
{
    public LessonsRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
