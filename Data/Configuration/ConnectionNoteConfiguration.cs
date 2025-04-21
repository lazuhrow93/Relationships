using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration;

public class ConnectionNoteConfiguration : IEntityTypeConfiguration<ConnectionNote>
{
    public void Configure(EntityTypeBuilder<ConnectionNote> builder)
    {
        builder
            .HasOne(e => e.Connection)
            .WithMany(c => c.Notes)
            .HasForeignKey(e => e.ConnectionId);

        builder
            .Property(e => e.Content)
            .HasMaxLength(500)
            .IsRequired();
    }
}
