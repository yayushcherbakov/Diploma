using TangoSchool.ApplicationServices.Models.Teachers;

namespace TangoSchool.ApplicationServices.Services.Interfaces;

public interface ITeacherService
{
    Task<List<TeacherHeader>> GetTeacherHeaders
    (
        CancellationToken cancellationToken
    );
}
