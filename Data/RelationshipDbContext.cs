using Microsoft.EntityFrameworkCore;

namespace Data;

public class RelationshipDbContext : DbContext
{
    public RelationshipDbContext(DbContextOptions<RelationshipDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RelationshipDbContext).Assembly);

        // Convert all table names to snake_case
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Convert table name
            var tableName = entity.GetTableName();
            if (tableName != null)
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }
        }
    }

    #region Private Helpers

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;

        return string.Concat(
            input.Select((c, i) =>
                i > 0 && char.IsUpper(c) ? "_" + char.ToLower(c) : char.ToLower(c).ToString())
        );
    }

    #endregion
}
