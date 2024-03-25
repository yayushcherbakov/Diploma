using TangoSchool.DataAccess.Enums;

namespace TangoSchool.ApplicationServices.Models.Identities;

public class RegisterStudentPayload : RegisterUserPayload
{
    public DanceProficiencyLevel Level { get; set; }
}
