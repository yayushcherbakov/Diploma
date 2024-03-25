using AuditService.DataAccess.Enums;

namespace AuditService.ApplicationServices.Models.Identities;

public class RegisterStudentPayload : RegisterUserPayload
{
    public DanceProficiencyLevel Level { get; set; }
}
