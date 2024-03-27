using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.SubscriptionTemplates;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class SubscriptionTemplatesService : ISubscriptionTemplatesService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;
    private readonly ISubscriptionTemplatesRepository _subscriptionTemplatesRepository;

    public SubscriptionTemplatesService
    (
        IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext,
        ISubscriptionTemplatesRepository subscriptionTemplatesRepository
    )
    {
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
        _subscriptionTemplatesRepository = subscriptionTemplatesRepository;
    }

    public async Task<Guid> CreateSubscriptionTemplate
        (CreateSubscriptionTemplatePayload payload, CancellationToken cancellationToken)
    {
        var newSubscriptionTemplate = _subscriptionTemplatesRepository.Add(payload.MapToDatabaseSubscriptionTemplate());

        await _subscriptionTemplatesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return newSubscriptionTemplate.Id;
    }

    public async Task UpdateSubscriptionTemplate
        (UpdateSubscriptionTemplate payload, CancellationToken cancellationToken)
    {
        var subscriptionTemplate = await _readOnlyTangoSchoolDbContext
            .SubscriptionTemplates
            .Where(x => x.Id == payload.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (subscriptionTemplate is null)
        {
            throw new ApplicationException(GeneralErrorMessages.SubscriptionTemplateWasNotFound);
        }

        subscriptionTemplate.Name = payload.Name;
        subscriptionTemplate.Description = payload.Description;
        subscriptionTemplate.LessonType = payload.LessonType;
        subscriptionTemplate.LessonCount = payload.LessonCount;
        subscriptionTemplate.ExpirationDate = payload.ExpirationDate;
        subscriptionTemplate.ExpirationDayCount = payload.ExpirationDayCount;
        subscriptionTemplate.Price = payload.Price;
        subscriptionTemplate.Active = payload.Active;

        _subscriptionTemplatesRepository.Update(subscriptionTemplate);

        await _subscriptionTemplatesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetSubscriptionTemplateResponse> GetSubscriptionTemplate
        (Guid id, CancellationToken cancellationToken)
    {
        var subscriptionTemplate = await _readOnlyTangoSchoolDbContext
            .SubscriptionTemplates
            .Where(x => x.Id == id)
            .Select(x => new GetSubscriptionTemplateResponse(
                x.Name,
                x.Description,
                x.LessonType,
                x.LessonCount,
                x.ExpirationDate,
                x.ExpirationDayCount,
                x.Price,
                x.Active))
            .SingleOrDefaultAsync(cancellationToken);

        if (subscriptionTemplate is null)
        {
            throw new ApplicationException(GeneralErrorMessages.SubscriptionTemplateWasNotFound);
        }

        return subscriptionTemplate;
    }
}
