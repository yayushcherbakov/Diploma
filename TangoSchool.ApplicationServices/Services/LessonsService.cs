using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.Lessons;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class LessonsService : ILessonsService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;
    private readonly ILessonsRepository _lessonsRepository;

    public LessonsService
    (
        IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext,
        ILessonsRepository lessonsRepository
    )
    {
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
        _lessonsRepository = lessonsRepository;
    }

    public async Task<Guid> CreateLesson(CreateLessonPayload payload, CancellationToken cancellationToken)
    {
        var newLesson = _lessonsRepository.Add(payload.MapToDatabaseLesson());

        await _lessonsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return newLesson.Id;
    }

    public async Task UpdateLesson(UpdateLesson payload, CancellationToken cancellationToken)
    {
        var lesson = await _readOnlyTangoSchoolDbContext
            .Lessons
            .Where(x => x.Id == payload.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (lesson is null)
        {
            throw new ApplicationException(GeneralErrorMessages.LessonWasNotFound);
        }

        lesson.Name = payload.Name;
        lesson.Description = payload.Description;
        lesson.LessonType = payload.LessonType;
        lesson.StartTime = payload.StartTime;
        lesson.FinishTime = payload.FinishTime;
        lesson.ClassroomId = payload.ClassroomId;
        lesson.TeacherId = payload.TeacherId;
        lesson.StudentId = payload.StudentId;
        lesson.GroupId = payload.GroupId;

        _lessonsRepository.Update(lesson);

        await _lessonsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetLessonResponse> GetLesson(Guid id, CancellationToken cancellationToken)
    {
        var lesson = await _readOnlyTangoSchoolDbContext
            .Lessons
            .Where(x => x.Id == id)
            .Select(x => new GetLessonResponse(
                x.Name,
                x.Description,
                x.LessonType,
                x.StartTime,
                x.FinishTime,
                x.ClassroomId,
                x.TeacherId,
                x.StudentId,
                x.GroupId))
            .SingleOrDefaultAsync(cancellationToken);

        if (lesson is null)
        {
            throw new ApplicationException(GeneralErrorMessages.LessonWasNotFound);
        }

        return lesson;
    }
}
