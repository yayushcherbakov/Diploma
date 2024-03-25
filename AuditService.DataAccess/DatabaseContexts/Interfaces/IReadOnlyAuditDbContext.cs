using AuditService.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuditService.DataAccess.DatabaseContexts.Interfaces;

public interface IReadOnlyAuditDbContext
{
    public DbSet<ApplicationUser> Users { get; }
    
    public DbSet<IdentityRole<Guid>> Roles { get; }
    
    public DbSet<IdentityUserRole<Guid>> UserRoles { get; }
    
    public DbSet<AuditLog> AuditLogs { get; }
    
    public DbSet<Classroom> Classrooms { get; }
    
    public DbSet<Group> Groups { get; }
    
    public DbSet<Lesson> Lessons { get; }
    
    public DbSet<LessonRequest> LessonRequests { get; }
    
    public DbSet<Student> Students { get; }
    
    public DbSet<StudentGroup> StudentGroups { get; }
    
    public DbSet<Subscription> Subscriptions { get; }
    
    public DbSet<SubscriptionTemplate> SubscriptionTemplates { get; }
    
    public DbSet<Teacher> Teachers { get; }
    
    public DbSet<Administrator> Administrators { get; }
}
