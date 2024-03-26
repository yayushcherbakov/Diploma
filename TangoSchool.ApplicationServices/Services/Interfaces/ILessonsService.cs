using TangoSchool.ApplicationServices.Models.Lessons;

namespace TangoSchool.ApplicationServices.Services.Interfaces;

public interface ILessonsService
{
    Task<Guid> CreateLesson
    (
        CreateLessonPayload payload,
        CancellationToken cancellationToken
    );

    Task UpdateLesson
    (
        UpdateLesson payload,
        CancellationToken cancellationToken
    );

    Task<GetLessonResponse> GetLesson
    (
        Guid id,
        CancellationToken cancellationToken
    );
}
