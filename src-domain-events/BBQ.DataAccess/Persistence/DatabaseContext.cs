﻿using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BBQ.DataAccess.Common;
using BBQ.DataAccess.Entities;
using BBQ.DataAccess.Identity;
using BBQ.DataAccess.Services;

namespace BBQ.DataAccess.Persistence;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    private readonly IClaimService _claimService;
    private readonly IPublisher _mediator;

    public DatabaseContext(DbContextOptions options, IClaimService claimService, IPublisher mediator) : base(options)
    {
        _claimService = claimService;
        _mediator = mediator;
    }

    public DbSet<SessionNote> SessionNotes { get; set; }

    public DbSet<BbqSession> BbqSessions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
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

        var domainEvents = this.GetDomainEvents();
        var rowsAffected = await base.SaveChangesAsync(cancellationToken);
        await _mediator.DispatchDomainEvents(domainEvents);
        return rowsAffected;
    }
}
