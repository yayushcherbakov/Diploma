using AuditService.DataAccess.DatabaseContexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuditService.DataAccess.DatabaseContexts;

internal sealed class ReadOnlyAuditDbContext : AuditDbContext, IReadOnlyAuditDbContext
{
    public ReadOnlyAuditDbContext(DbContextOptions<AuditDbContext> options)
        : base(options)
    { }
    
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }
    
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync
    (
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default
    )
    {
        throw new InvalidOperationException("This context is read-only.");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        
        base.OnConfiguring(optionsBuilder);
    }
}
