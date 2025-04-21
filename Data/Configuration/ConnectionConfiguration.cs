using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration;

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder
            .HasOne(e => e.SourceCharacter)
            .WithMany(c => c.SourceConnections)
            .HasForeignKey(e => e.SourceCharacterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.TargetCharacter)
            .WithMany(c => c.TargetConnections)
            .HasForeignKey(e => e.TargetCharacterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
