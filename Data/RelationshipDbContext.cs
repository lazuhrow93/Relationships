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

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Table name to snake_case
            var tableName = entity.GetTableName();
            if (tableName != null)
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }

            // Column names to snake_case
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            foreach (var key in entity.GetKeys())
            {
                if(tableName != null)
                {
                    key.SetName($"pk_{ToSnakeCase(tableName)}");
                }
            }

            // Foreign key constraint names to snake_case
            foreach (var fk in entity.GetForeignKeys())
            {
                if(tableName != null)
                {
                    // Get the name of the foreign key property (e.g., "UserId")
                    var fkPropertyName = ToSnakeCase(fk.Properties.First().Name);

                    // Get the principal (referenced) table
                    var principalTable = ToSnakeCase(fk.PrincipalEntityType.GetTableName()!);

                    // Set the foreign key constraint name based on the property name
                    fk.SetConstraintName($"fk_{ToSnakeCase(tableName!)}_{fkPropertyName}_{principalTable}");
                }
            }

            // Index names to snake_case
            foreach (var index in entity.GetIndexes())
            {
                if(tableName != null)
                {
                    var columnNames = string.Join("_", index.Properties
                        .Select(p => ToSnakeCase(p.Name)));
                    index.SetDatabaseName($"ix_{ToSnakeCase(tableName)}_{columnNames}");
                }
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
