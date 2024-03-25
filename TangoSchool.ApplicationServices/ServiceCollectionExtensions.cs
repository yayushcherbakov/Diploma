using Microsoft.Extensions.DependencyInjection;
using TangoSchool.ApplicationServices.Services;
using TangoSchool.ApplicationServices.Services.Interfaces;

namespace TangoSchool.ApplicationServices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices
    (
        this IServiceCollection services
    )
        => services
            .AddScoped<IIdentityService, IdentityService>()
            .AddScoped<IAuditLogService, AuditLogService>()
            .AddScoped<IClassroomsService, ClassroomsService>()
            .AddScoped<IEmailSender, EmailSender>();
}
