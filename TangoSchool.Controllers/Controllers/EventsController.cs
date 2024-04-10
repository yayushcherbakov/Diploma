using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Events;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateEvente
    (
        [FromBody] CreateEventPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _eventService.CreateEvent(payload, cancellationToken));
    }

    [Authorize(Roles = RoleConstants.Administrator)]
    [HttpPut("Update")]
    public async Task<ActionResult> UpdateEvent
    (
        [FromBody] UpdateEventPayload payload,
        CancellationToken cancellationToken
    )
    {
        await _eventService.UpdateEvent(payload, cancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetEventResponse>> GetEvent
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _eventService.GetEvent(id, cancellationToken));
    }

    [HttpGet("All")]
    public async Task<ActionResult<GetAllEventsResponse>> GetAllEvents
    (
        [FromQuery] GetAllEventsPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _eventService.GetAllEvents(payload, cancellationToken));
    }
}
