using System.Net;
using System.Net.Mail;
using AuditService.ApplicationServices.Constants;
using AuditService.ApplicationServices.Models.Emails;
using AuditService.ApplicationServices.Options;
using AuditService.ApplicationServices.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AuditService.ApplicationServices.Services;

internal class EmailSender : IEmailSender
{
    private readonly EmailSenderOptions _emailSenderOptions;

    public EmailSender(IOptions<EmailSenderOptions> emailSenderOptions)
    {
        _emailSenderOptions = emailSenderOptions.Value;
    }

    public async Task SendEmail
    (
        SendEmailPayload payload,
        CancellationToken cancellationToken
    )
    {
        using var client = new SmtpClient(_emailSenderOptions.Host, _emailSenderOptions.Port);

        client.Credentials = new NetworkCredential
        {
            UserName = _emailSenderOptions.From,
            Password = _emailSenderOptions.Password,
        };

        client.EnableSsl = true;

        var message = new MailMessage(_emailSenderOptions.From, payload.Recipient)
        {
            Subject = payload.Subject,
            Body = payload.Body,
            IsBodyHtml = true
        };

        try
        {
            await client.SendMailAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(GeneralErrorMessages.MailSendingFailed);
        }
    }
}
