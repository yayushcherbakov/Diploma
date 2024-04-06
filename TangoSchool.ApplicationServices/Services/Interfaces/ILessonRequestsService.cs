using TangoSchool.ApplicationServices.Models.LessonRequests;

namespace TangoSchool.ApplicationServices.Services.Interfaces;

public interface ILessonRequestsService
{
    Task<Guid> CreateLessonRequest
    (
        CreateLessonRequestPayload payload,
        CancellationToken cancellationToken
    );

    Task UpdateLessonRequest
    (
        UpdateLessonRequest payload,
        CancellationToken cancellationToken
    );

    Task<GetLessonRequestResponse> GetLessonRequest
    (
        Guid id,
        CancellationToken cancellationToken
    );
    
    Task<GetLessonRequestByTeacherResponse> GetLessonRequestsByTeacher
    (
        Guid teacherId,
        GetLessonRequestByTeacherPayload payload,
        CancellationToken cancellationToken
    );
}
