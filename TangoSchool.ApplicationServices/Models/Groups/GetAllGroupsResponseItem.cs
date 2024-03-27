using TangoSchool.DataAccess.Enums;

namespace TangoSchool.ApplicationServices.Models.Groups;

public record GetAllGroupsResponseItem
(
    Guid Id,
    string Name,
    string? Description,
    DanceProficiencyLevel Level,
    int MaxStudentCapacity,
    Guid TeacherId,
    bool Terminated
);
