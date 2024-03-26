using TangoSchool.ApplicationServices.Models.Classrooms;
using TangoSchool.ApplicationServices.Models.Subscriptions;

namespace TangoSchool.ApplicationServices.Services.Interfaces;

public interface ISubscriptionsService
{
    Task<Guid> CreateSubscription
    (
        CreateSubscriptionPayload payload,
        CancellationToken cancellationToken
    );

    Task UpdateSubscription
    (
        UpdateSubscription payload,
        CancellationToken cancellationToken
    );

    Task<GetSubscriptionResponse> GetSubscription
    (
        Guid id,
        CancellationToken cancellationToken
    );
}
