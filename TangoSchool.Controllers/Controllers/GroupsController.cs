using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Groups;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(Roles = RoleConstants.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Groups")]
public class GroupsController : ControllerBase
{
    private readonly IGroupsService _groupsService;

    public GroupsController(IGroupsService groupsService)
    {
        _groupsService = groupsService;
    }

    [HttpPost("Metadata")]
    public async Task<ActionResult<GroupsMetadata>> GetGroupsMetadata
    (
        CancellationToken cancellationToken
    )
    {
        return Ok(await _groupsService.GetGroupsMetadata(cancellationToken));
    }
    
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateGroup
    (
        [FromBody] CreateGroupPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _groupsService.CreateGroup(payload, cancellationToken));
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateGroup
    (
        [FromBody] UpdateGroupPayload payload,
        CancellationToken cancellationToken
    )
    {
        await _groupsService.UpdateGroup(payload, cancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetGroupResponse>> GetGroup
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _groupsService.GetGroup(id, cancellationToken));
    }

    [HttpGet("All")]
    public async Task<ActionResult<GetAllGroupsResponse>> GetAllGroups
    (
        [FromQuery] GetAllGroupsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _groupsService.GetAllGroups(payload, cancellationToken));
    }

    [HttpGet("Headers")]
    public async Task<ActionResult<List<GroupHeader>>> GetGroupHeaders
    (
        CancellationToken cancellationToken
    )
    {
        return Ok(await _groupsService.GetGroupHeaders(cancellationToken));
    }

    [HttpPost("{id:guid}/Terminate")]
    public async Task<ActionResult> TerminateGroup
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _groupsService.TerminateGroup(id, cancellationToken);

        return Ok();
    }

    [HttpPost("{id:guid}/Restore")]
    public async Task<ActionResult> RestoreGroup
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _groupsService.RestoreGroup(id, cancellationToken);

        return Ok();
    }

    [HttpPost("{id:guid}/Students/Add/{studentId:guid}")]
    public async Task<ActionResult> AddStudentToGroup
    (
        [FromRoute] Guid id,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken
    )
    {
        await _groupsService.AddStudentToGroup(id, studentId, cancellationToken);

        return Ok();
    }

    [HttpPost("{id:guid}/Students/Remove/{studentId:guid}")]
    public async Task<ActionResult> RemoveStudentFromGroup
    (
        [FromRoute] Guid id,
        [FromRoute] Guid studentId,
        CancellationToken cancellationToken
    )
    {
        await _groupsService.RemoveStudentFromGroup(id, studentId, cancellationToken);

        return Ok();
    }
}
