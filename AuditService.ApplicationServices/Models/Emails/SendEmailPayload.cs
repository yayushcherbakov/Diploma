namespace AuditService.ApplicationServices.Models.Emails;

public record SendEmailPayload
(
    string Subject,
    string Body,
    string Recipient
);
