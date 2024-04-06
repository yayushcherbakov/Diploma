using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Models.Subscriptions;
using TangoSchool.ApplicationServices.Models.SubscriptionTemplates;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.Controllers;

[ApiController]
[Authorize(Roles = RoleConstants.Administrator, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("SubscriptionTemplates")]
public class SubscriptionTemplatesController : ControllerBase
{
    private readonly ISubscriptionTemplatesService _subscriptionTemplatesService;

    public SubscriptionTemplatesController(ISubscriptionTemplatesService subscriptionTemplatesService)
    {
        _subscriptionTemplatesService = subscriptionTemplatesService;
    }

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> CreateSubscriptionTemplate
    (
        [FromBody] CreateSubscriptionTemplatePayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _subscriptionTemplatesService.CreateSubscriptionTemplate(payload, cancellationToken));
    }

    [HttpPut("Update")]
    public async Task<ActionResult> UpdateSubscriptionTemplate
    (
        [FromBody] UpdateSubscriptionTemplate payload,
        CancellationToken cancellationToken
    )
    {
        await _subscriptionTemplatesService.UpdateSubscriptionTemplate(payload, cancellationToken);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetSubscriptionResponse>> GetSubscription
    (
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _subscriptionTemplatesService.GetSubscriptionTemplate(id, cancellationToken));
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<GetAllSubscriptionTemplatesResponse>> GetAllSubscriptionTemplates
    (
        [FromQuery] GetAllSubscriptionTemplatesPayload payload,
        CancellationToken cancellationToken
    )
    {
        return Ok(await _subscriptionTemplatesService.GetAllSubscriptionTemplates(payload, cancellationToken));
    }
}
