using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Extensions;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.LessonRequests;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Entities;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class LessonRequestsService : ILessonRequestsService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;
    private readonly ILessonRequestsRepository _lessonRequestsRepository;

    public LessonRequestsService
    (
        IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext,
        ILessonRequestsRepository lessonRequestsRepository
    )
    {
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
        _lessonRequestsRepository = lessonRequestsRepository;
    }

    public async Task<Guid> CreateLessonRequest(CreateLessonRequestPayload payload, CancellationToken cancellationToken)
    {
        var newLessonRequest = _lessonRequestsRepository.Add(payload.MapToDatabaseLesson());

        await _lessonRequestsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return newLessonRequest.Id;
    }

    public async Task UpdateLessonRequest(UpdateLessonRequest payload, CancellationToken cancellationToken)
    {
        var lessonRequest = await _readOnlyTangoSchoolDbContext
            .LessonRequests
            .Where(x => x.Id == payload.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (lessonRequest is null)
        {
            throw new ApplicationException(GeneralErrorMessages.LessonRequestWasNotFound);
        }

        lessonRequest.Description = payload.Description;
        lessonRequest.StartTime = payload.StartTime;
        lessonRequest.FinishTime = payload.FinishTime;
        lessonRequest.StudentId = payload.StudentId;
        lessonRequest.TeacherId = payload.TeacherId;

        _lessonRequestsRepository.Update(lessonRequest);

        await _lessonRequestsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetLessonRequestResponse> GetLessonRequest(Guid id, CancellationToken cancellationToken)
    {
        var lessonRequest = await _readOnlyTangoSchoolDbContext
            .LessonRequests
            .Where(x => x.Id == id)
            .Select(x => new GetLessonRequestResponse(
                x.Description,
                x.StartTime,
                x.FinishTime,
                x.StudentId,
                x.TeacherId))
            .SingleOrDefaultAsync(cancellationToken);

        if (lessonRequest is null)
        {
            throw new ApplicationException(GeneralErrorMessages.LessonRequestWasNotFound);
        }

        return lessonRequest;
    }

    public async Task<GetLessonRequestByTeacherResponse> GetLessonRequestsByTeacher
    (
        Guid teacherId,
        GetLessonRequestByTeacherPayload payload,
        CancellationToken cancellationToken
    )
    {
        IQueryable<LessonRequest> query = _readOnlyTangoSchoolDbContext.LessonRequests;

        query = query
            .FilterActive()
            .Where(x => x.TeacherId == teacherId);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .Paginate(payload.ItemsPerPage, payload.Page)
            .Select(x => new GetLessonRequestByTeacherResponseItem(
                x.Description,
                x.StartTime,
                x.FinishTime,
                x.StudentId,
                x.TeacherId
            ))
            .ToListAsync(cancellationToken);

        return new(result, totalCount);
    }
}
