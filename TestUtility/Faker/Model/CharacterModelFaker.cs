using Domain.Models.Entities;

namespace TestUtility.Faker.Model;
public class CharacterModelFaker : RelationshipFaker<CharacterModel, CharacterModelFaker>
{
    public CharacterModelFaker()
    {
        RuleFor(x => x.UserId, f => f.Random.Int(1, 100));
        RuleFor(x => x.Name, f => f.Name.FirstName());
    }
}
