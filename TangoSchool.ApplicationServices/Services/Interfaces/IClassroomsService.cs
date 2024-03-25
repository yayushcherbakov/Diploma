using TangoSchool.ApplicationServices.Models.Classrooms;

namespace TangoSchool.ApplicationServices.Services.Interfaces;

public interface IClassroomsService
{
    Task<Guid> CreateClassroom
    (
        CreateClassroomPayload payload,
        CancellationToken cancellationToken
    );

    Task UpdateClassroom
    (
        UpdateClassroom payload,
        CancellationToken cancellationToken
    );

    Task<GetClassroomResponse> GetClassroom
    (
        Guid id,
        CancellationToken cancellationToken
    );
}
