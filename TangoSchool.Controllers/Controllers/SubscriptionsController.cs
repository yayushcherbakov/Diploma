using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Subscriptions;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(Roles = RoleConstants.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("Subscriptions")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionsService _subscriptionsService;

    public SubscriptionsController(ISubscriptionsService subscriptionsService)
    {
        _subscriptionsService = subscriptionsService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateSubscription
    (
        [FromBody] CreateSubscriptionPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _subscriptionsService.CreateSubscription(payload, cancellationToken));
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateSubscription
    (
        [FromBody] UpdateSubscription payload,
        CancellationToken cancellationToken
    )
    {
        await _subscriptionsService.UpdateSubscription(payload, cancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetSubscriptionResponse>> GetSubscription
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _subscriptionsService.GetSubscription(id, cancellationToken));
    }
}
