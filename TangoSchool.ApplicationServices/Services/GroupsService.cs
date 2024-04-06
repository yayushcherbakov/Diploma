using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Extensions;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.Groups;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Entities;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class GroupsService : IGroupsService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;
    private readonly IGroupsRepository _groupsRepository;

    public GroupsService
    (
        IGroupsRepository groupsRepository,
        IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext
    )
    {
        _groupsRepository = groupsRepository;
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
    }

    public async Task<Guid> CreateGroup(CreateGroupPayload payload, CancellationToken cancellationToken)
    {
        var newGroup = _groupsRepository.Add(payload.MapToDatabaseGroup());

        await _groupsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return newGroup.Id;
    }

    public async Task UpdateGroup(UpdateGroup payload, CancellationToken cancellationToken)
    {
        var group = await _readOnlyTangoSchoolDbContext
            .Groups
            .Where(x => x.Id == payload.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (group is null)
        {
            throw new ApplicationException(GeneralErrorMessages.GroupWasNotFound);
        }

        group.Name = payload.Name;
        group.Description = payload.Description;
        group.Level = payload.Level;
        group.MaxStudentCapacity = payload.MaxStudentCapacity;
        group.TeacherId = payload.TeacherId;

        _groupsRepository.Update(group);

        await _groupsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetGroupResponse> GetGroup(Guid id, CancellationToken cancellationToken)
    {
        var group = await _readOnlyTangoSchoolDbContext
            .Groups
            .Where(x => x.Id == id)
            .Select(x => new GetGroupResponse(
                x.Name,
                x.Description,
                x.Level,
                x.MaxStudentCapacity,
                x.TeacherId))
            .SingleOrDefaultAsync(cancellationToken);

        if (group is null)
        {
            throw new ApplicationException(GeneralErrorMessages.GroupWasNotFound);
        }

        return group;
    }

    public async Task<GetAllGroupsResponse> GetAllGroups
    (
        GetAllGroupsPayload payload,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Group> query = _readOnlyTangoSchoolDbContext.Groups;

        if (!payload.IncludeTerminated)
        {
            query = query.Where(x => !x.Terminated);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .Paginate(payload.ItemsPerPage, payload.Page)
            .Select(x => new GetAllGroupsResponseItem(
                x.Id,
                x.Name,
                x.Description,
                x.Level,
                x.MaxStudentCapacity,
                x.TeacherId,
                x.Terminated))
            .ToListAsync(cancellationToken);

        return new(result, totalCount);
    }
    
    public async Task<List<GroupHeader>> GetGroupHeaders
    (
        CancellationToken cancellationToken
    )
    {
        return await _readOnlyTangoSchoolDbContext
            .Groups
            .FilterActive()
            .Select(x => new GroupHeader
            (
                x.Id,
                x.Name
            ))
            .ToListAsync(cancellationToken);
    }
}
