using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Entities;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class AdministratorsRepository : BaseRepository<Administrator>, IAdministratorsRepository
{
    public AdministratorsRepository(AuditDbContext auditDbContext) : base(auditDbContext)
    {
    }
}
