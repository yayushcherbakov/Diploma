using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class StudentsRepository : BaseRepository<Student>, IStudentsRepository
{
    public StudentsRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
