using TangoSchool.DataAccess.Enums;

namespace TangoSchool.ApplicationServices.Models.Groups;

public record GetGroupResponse
(
    string Name,
    string? Description,
    DanceProficiencyLevel Level,
    int MaxStudentCapacity,
    Guid TeacherId
);
