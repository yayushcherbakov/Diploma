using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.Subscriptions;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class SubscriptionsService : ISubscriptionsService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public SubscriptionsService
    (
        IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext,
        ISubscriptionsRepository subscriptionsRepository
    )
    {
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<Guid> CreateSubscription(CreateSubscriptionPayload payload, CancellationToken cancellationToken)
    {
        var newSubscription = _subscriptionsRepository.Add(payload.MapToDatabaseSubscription());

        await _subscriptionsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return newSubscription.Id;
    }

    public async Task UpdateSubscription(UpdateSubscription payload, CancellationToken cancellationToken)
    {
        var subscription = await _readOnlyTangoSchoolDbContext
            .Subscriptions
            .Where(x => x.Id == payload.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (subscription is null)
        {
            throw new ApplicationException(GeneralErrorMessages.SubscriptionWasNotFound);
        }

        subscription.Name = payload.Name;
        subscription.Description = payload.Description;
        subscription.LessonType = payload.LessonType;
        subscription.LessonCount = payload.LessonCount;
        subscription.ExpirationDate = payload.ExpirationDate;
        subscription.Price = payload.Price;
        subscription.StudentId = payload.StudentId;

        _subscriptionsRepository.Update(subscription);

        await _subscriptionsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetSubscriptionResponse> GetSubscription(Guid id, CancellationToken cancellationToken)
    {
        var subscription = await _readOnlyTangoSchoolDbContext
            .Subscriptions
            .Where(x => x.Id == id)
            .Select(x => new GetSubscriptionResponse(
                x.Name,
                x.Description,
                x.LessonType,
                x.LessonCount,
                x.ExpirationDate,
                x.Price,
                x.StudentId))
            .SingleOrDefaultAsync(cancellationToken);

        if (subscription is null)
        {
            throw new ApplicationException(GeneralErrorMessages.SubscriptionWasNotFound);
        }

        return subscription;
    }
}
