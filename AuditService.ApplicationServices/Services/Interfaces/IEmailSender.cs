using AuditService.ApplicationServices.Models.Emails;

namespace AuditService.ApplicationServices.Services.Interfaces;

public interface IEmailSender
{
    Task SendEmail
    (
        SendEmailPayload payload,
        CancellationToken cancellationToken
    );
}
