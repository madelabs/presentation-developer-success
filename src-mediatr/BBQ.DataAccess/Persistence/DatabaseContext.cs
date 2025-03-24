using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BBQ.DataAccess.Common;
using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Identity;
using BBQ.DataAccess.Services;
using BBQ.DataAccess.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BBQ.DataAccess.Persistence;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    private readonly IClaimService _claimService;

    public DatabaseContext(DbContextOptions options, IClaimService claimService) : base(options)
    {
        _claimService = claimService;
    }

    public DbSet<SessionNote> SessionNotes { get; set; }

    public DbSet<BbqSession> BbqSessions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        var pitTempConverter = new ValueConverter<PitTemperature, decimal>(
            v => v.Value, 
            v => new PitTemperature(v) 
        );

        modelBuilder.Entity<SessionNote>()
            .Property(e => e.PitTemperature)
            .HasConversion(pitTempConverter)
            .HasColumnName("PitTemperature");
        
        var meatTempConverter = new ValueConverter<MeatTemperature, decimal>(
            v => v.Value, 
            v => new MeatTemperature(v) 
        );

        modelBuilder.Entity<SessionNote>()
            .Property(e => e.MeatTemperature)
            .HasConversion(meatTempConverter)
            .HasColumnName("MeatTemperature");
        
        base.OnModelCreating(modelBuilder);
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _claimService.GetUserId();
                    entry.Entity.CreatedOn = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _claimService.GetUserId();
                    entry.Entity.UpdatedOn = DateTime.Now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
