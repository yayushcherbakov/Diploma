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
        [FromBody] UpdateGroup payload,
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
    
    [HttpGet("GetAll")]
    public async Task<ActionResult<GetAllGroupsResponse>> GetAllGroups
    (
        [FromQuery] GetAllGroupsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _groupsService.GetAllGroups(payload, cancellationToken));
    }
}
