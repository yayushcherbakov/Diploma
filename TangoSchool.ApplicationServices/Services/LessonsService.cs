using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.Classrooms;
using TangoSchool.ApplicationServices.Models.Lessons;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Enums;
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
        switch (payload.LessonType)
        {
            case LessonType.Individual when !payload.StudentId.HasValue || payload.GroupId.HasValue:
            {
                throw new ApplicationException(GeneralErrorMessages.InvalidLessonData);
            }
            case LessonType.Group when payload.StudentId.HasValue || !payload.GroupId.HasValue:
            {
                throw new ApplicationException(GeneralErrorMessages.InvalidLessonData);
            }
            case LessonType.Seminar when payload is {StudentId: not null, GroupId: not null}:
            {
                throw new ApplicationException(GeneralErrorMessages.InvalidLessonData);
            }
            default:
            {
                break;
            }
        }

        if (payload.StartTime >= payload.FinishTime)
        {
            throw new ApplicationException(GeneralErrorMessages.StartTimeMustBeLessThanFinishTime);
        }

        var timePeriodAvailable = await _readOnlyTangoSchoolDbContext
            .Lessons
            .Where(x => x.ClassroomId == payload.ClassroomId)
            .AllAsync(x => (x.StartTime <= payload.StartTime && x.StartTime <= payload.FinishTime)
                    || (x.StartTime >= payload.StartTime && x.FinishTime >= payload.FinishTime),
                cancellationToken);

        if (!timePeriodAvailable)
        {
            throw new ApplicationException(GeneralErrorMessages.TimePeriodIsNotAvailable);
        }

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
