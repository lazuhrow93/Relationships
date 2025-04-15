using Entities;

namespace TestUtility.Faker.Entities;

public class EntityFaker<T, TFaker> : RelationshipFaker<T, TFaker>
    where T : Entity
    where TFaker : EntityFaker<T, TFaker>
{
    public EntityFaker()
    {
        RuleFor(x => x.Id, f => f.IndexFaker);
    }
}
