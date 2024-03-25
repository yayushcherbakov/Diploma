namespace AuditService.ApplicationServices.Models.Identities;

public record ConfirmResetPasswordPayload
(
    string Email,
    string NewPassword,
    string ResetPasswordToken
);
