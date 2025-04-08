using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration;

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder
            .HasOne(e => e.CharacterStart)
            .WithMany(c => c.StartConnections)
            .HasForeignKey(e => e.CharacterStartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.CharacterEnd)
            .WithMany(c => c.EndConnections)
            .HasForeignKey(e => e.CharacterEndId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
