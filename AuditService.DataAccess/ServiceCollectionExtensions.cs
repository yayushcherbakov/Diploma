using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.DatabaseContexts.Interfaces;
using AuditService.DataAccess.Repositories;
using AuditService.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.DataAccess;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess
    (
        this IServiceCollection services,
        string connectionString
    )
        => services
            .AddDbContext<AuditDbContext>(opt => opt.UseNpgsql(connectionString))
            .AddDbContext<ReadOnlyAuditDbContext>(opt => opt.UseNpgsql(connectionString))
            .AddScoped<IReadOnlyAuditDbContext, ReadOnlyAuditDbContext>()
            .AddScoped<IAuditDbContextMigrator, AuditDbContextMigrator>()
            .AddScoped<IStudentsRepository, StudentsRepository>()
            .AddScoped<IClassroomsRepository, ClassroomsRepository>()
            .AddScoped<IGroupsRepository, GroupsRepository>()
            .AddScoped<ILessonsRepository, LessonsRepository>()
            .AddScoped<ILessonsRepository, LessonsRepository>()
            .AddScoped<ISubscriptionsTemplatesRepository, SubscriptionsTemplatesRepository>()
            .AddScoped<ITeachersRepository, TeachersRepository>()
            .AddScoped<IAdministratorsRepository, AdministratorsRepository>()
            .AddScoped<IAuditLogsRepository, AuditLogsRepository>();

    public static IdentityBuilder AddDataAccess
    (
        this IdentityBuilder builder
    )
        => builder.AddEntityFrameworkStores<AuditDbContext>();
}
