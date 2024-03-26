using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.Classrooms;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class ClassroomsService : IClassroomsService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;
    private readonly IClassroomsRepository _classroomsRepository;

    public ClassroomsService
    (
        IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext,
        IClassroomsRepository classroomsRepository
    )
    {
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
        _classroomsRepository = classroomsRepository;
    }

    public async Task<Guid> CreateClassroom(CreateClassroomPayload payload, CancellationToken cancellationToken)
    {
        var newClassroom = _classroomsRepository.Add(payload.MapToDatabaseClassroom());

        await _classroomsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return newClassroom.Id;
    }

    public async Task UpdateClassroom(UpdateClassroom payload, CancellationToken cancellationToken)
    {
        var classroom = await _readOnlyTangoSchoolDbContext
            .Classrooms
            .Where(x => x.Id == payload.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (classroom is null)
        {
            throw new ApplicationException(GeneralErrorMessages.ClassroomWasNotFound);
        }

        classroom.Name = payload.Name;
        classroom.Description = payload.Description;

        _classroomsRepository.Update(classroom);

        await _classroomsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetClassroomResponse> GetClassroom(Guid id, CancellationToken cancellationToken)
    {
        var classroom = await _readOnlyTangoSchoolDbContext
            .Classrooms
            .Where(x => x.Id == id)
            .Select(x => new GetClassroomResponse(x.Name, x.Description))
            .SingleOrDefaultAsync(cancellationToken);

        if (classroom is null)
        {
            throw new ApplicationException(GeneralErrorMessages.ClassroomWasNotFound);
        }

        return classroom;
    }
}
