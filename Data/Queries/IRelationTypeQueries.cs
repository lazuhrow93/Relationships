using Entities;

namespace Data.Queries;

public interface IRelationTypeQueries : IEntityQueries<RelationType>
{
}

public class RelationTypeQueries : EntityQueries<RelationType>, IRelationTypeQueries
{
    public RelationTypeQueries(RelationshipDbContext dbContext) : base(dbContext)
    {
    }
}
