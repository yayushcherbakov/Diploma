using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class TeachersRepository : BaseRepository<Teacher>, ITeachersRepository
{
    public TeachersRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
