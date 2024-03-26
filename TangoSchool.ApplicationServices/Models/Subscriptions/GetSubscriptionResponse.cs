using TangoSchool.DataAccess.Enums;

namespace TangoSchool.ApplicationServices.Models.Subscriptions;

public record GetSubscriptionResponse
(
    string Name,
    string? Description,
    LessonType LessonType,
    int LessonCount,
    DateTimeOffset ExpirationDate,
    decimal Price,
    Guid StudentId
);
