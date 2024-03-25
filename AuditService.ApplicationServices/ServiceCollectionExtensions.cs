using AuditService.ApplicationServices.Services;
using AuditService.ApplicationServices.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.ApplicationServices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices
    (
        this IServiceCollection services
    )
        => services
            .AddScoped<IIdentityService, IdentityService>()
            .AddScoped<IAuditLogService, AuditLogService>()
            .AddScoped<IEmailSender, EmailSender>();
}
