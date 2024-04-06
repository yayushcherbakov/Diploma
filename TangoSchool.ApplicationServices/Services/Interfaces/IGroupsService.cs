using TangoSchool.ApplicationServices.Models.Groups;

namespace TangoSchool.ApplicationServices.Services.Interfaces;

public interface IGroupsService
{
    Task<Guid> CreateGroup
    (
        CreateGroupPayload payload,
        CancellationToken cancellationToken
    );

    Task UpdateGroup
    (
        UpdateGroup payload,
        CancellationToken cancellationToken
    );

    Task<GetGroupResponse> GetGroup
    (
        Guid id,
        CancellationToken cancellationToken
    );

    Task<GetAllGroupsResponse> GetAllGroups
    (
        GetAllGroupsPayload payload,
        CancellationToken cancellationToken
    );

    Task<List<GroupHeader>> GetGroupHeaders
    (
        CancellationToken cancellationToken
    );
}
