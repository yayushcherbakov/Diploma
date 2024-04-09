using Microsoft.EntityFrameworkCore;
using TangoSchool.ApplicationServices.Constants;
using TangoSchool.ApplicationServices.Extensions;
using TangoSchool.ApplicationServices.Mappers;
using TangoSchool.ApplicationServices.Models.Events;
using TangoSchool.ApplicationServices.Models.SubscriptionTemplates;
using TangoSchool.ApplicationServices.Services.Interfaces;
using TangoSchool.DataAccess.DatabaseContexts.Interfaces;
using TangoSchool.DataAccess.Entities;
using TangoSchool.DataAccess.Repositories.Interfaces;

namespace TangoSchool.ApplicationServices.Services;

internal class EventService : IEventService
{
    private readonly IReadOnlyTangoSchoolDbContext _readOnlyTangoSchoolDbContext;
    private readonly IEventRepository _eventRepository;

    public EventService
    (
        IReadOnlyTangoSchoolDbContext readOnlyTangoSchoolDbContext,
        IEventRepository eventRepository
    )
    {
        _readOnlyTangoSchoolDbContext = readOnlyTangoSchoolDbContext;
        _eventRepository = eventRepository;
    }

    public async Task<Guid> CreateEvent(CreateEventPayload payload, CancellationToken cancellationToken)
    {
        var newEvent = _eventRepository.Add(payload.MapToDatabaseEvent());

        await _eventRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return newEvent.Id;
    }

    public async Task UpdateEvent(UpdateEventPayload payload, CancellationToken cancellationToken)
    {
        // var event = await _readOnlyTangoSchoolDbContext
        //     .Where(x => x.Id == payload.Id)
        //     .SingleOrDefaultAsync(cancellationToken);
        //
        // if (event is null)
        // {
        //     throw new ApplicationException(GeneralErrorMessages.SubscriptionTemplateWasNotFound);
        // }
        //
        // event.Name = payload.Name;
        // event.Description = payload.Description;
        // event.LessonType = payload.LessonType;
        // event.LessonCount = payload.LessonCount;
        // event.ExpirationDate = payload.ExpirationDate;
        // event.ExpirationDayCount = payload.ExpirationDayCount;
        // event.Price = payload.Price;
        // event.Active = payload.Active;
        //
        // _eventRepository.Update(event);
        //
        // await _eventRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<GetEventResponse> GetEvent(Guid id, CancellationToken cancellationToken)
    {
        // var subscriptionTemplate = await _readOnlyTangoSchoolDbContext
        //     .SubscriptionTemplates
        //     .Where(x => x.Id == id)
        //     .Select(x => new GetSubscriptionTemplateResponse(
        //         x.Name,
        //         x.Description,
        //         x.LessonType,
        //         x.LessonCount,
        //         x.ExpirationDate,
        //         x.ExpirationDayCount,
        //         x.Price,
        //         x.Active))
        //     .SingleOrDefaultAsync(cancellationToken);
        //
        // if (subscriptionTemplate is null)
        // {
        //     throw new ApplicationException(GeneralErrorMessages.SubscriptionTemplateWasNotFound);
        // }
        //
        // return subscriptionTemplate;

        throw new NotImplementedException();
    }

    public async Task<GetAllEventsResponse> GetAllEvents(GetAllEventsPayload payload, CancellationToken cancellationToken)
    {
        // IQueryable<SubscriptionTemplate> query = _readOnlyTangoSchoolDbContext.SubscriptionTemplates;
        //
        // if (!payload.Active)
        // {
        //     query = query.Where(x => !x.Active);
        // }
        //
        // var totalCount = await query.CountAsync(cancellationToken);
        //
        // var result = await query
        //     .Paginate(payload.ItemsPerPage, payload.Page)
        //     .Select(x => new GetAllSubscriptionTemplatesResponseItem
        //     (
        //         x.Id,
        //         x.Name,
        //         x.Description,
        //         x.LessonType,
        //         x.LessonCount,
        //         x.ExpirationDate,
        //         x.ExpirationDayCount,
        //         x.Price,
        //         x.Active))
        //     .ToListAsync(cancellationToken);
        //
        // return new(result, totalCount);
        
        throw new NotImplementedException();
    }
}
