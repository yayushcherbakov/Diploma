using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Extensions;
using TangoSchool.ApplicationServices.Models.Teachers;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class TeacherService : ITeacherService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;

    public TeacherService(IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext)
    {
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
    }

    public async Task<List<TeacherHeader>> GetTeacherHeaders
    (
        CancellationToken cancellationToken
    )
    {
        return await _readOnlyTangoSchoolDbContext
            .Teachers
            .FilterActive()
            .Select(x => new TeacherHeader
            (
                x.Id,
                x.ApplicationUser.FirstName,
                x.ApplicationUser.LastName,
                x.ApplicationUser.MiddleName
            ))
            .ToListAsync(cancellationToken);
    }
}
