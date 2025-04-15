using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration;

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder
            .HasOne(e => e.CharacterOne)
            .WithMany(c => c.CharacterConnectionsOne)
            .HasForeignKey(e => e.CharacterOneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.CharacterTwo)
            .WithMany(c => c.CharacterConnectionsTwo)
            .HasForeignKey(e => e.CharacterTwoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
