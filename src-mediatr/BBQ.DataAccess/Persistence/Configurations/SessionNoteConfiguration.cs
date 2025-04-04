﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BBQ.DataAccess.Entities;

namespace BBQ.DataAccess.Persistence.Configurations;

public class SessionNoteConfiguration : IEntityTypeConfiguration<SessionNote>
{
    public void Configure(EntityTypeBuilder<SessionNote> builder)
    {
        builder.Property(ti => ti.ActivityDescription)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ti => ti.Note)
            .HasMaxLength(1000)
            .IsRequired();
    }
}
