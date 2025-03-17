using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BBQ.DataAccess.Entities;

namespace BBQ.DataAccess.Persistence.Configurations;

public class BbqSessionConfiguration : IEntityTypeConfiguration<BbqSession>
{
    public void Configure(EntityTypeBuilder<BbqSession> builder)
    {
        builder.Property(tl => tl.Description)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(tl => tl.Result)
            .HasMaxLength(50);
        
        builder.HasMany(tl => tl.Notes)
            .WithOne(ti => ti.Session)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
