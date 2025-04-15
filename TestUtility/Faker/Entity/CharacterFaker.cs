using Entities;

namespace TestUtility.Faker.Entities;

public class CharacterFaker : EntityFaker<Character, CharacterFaker>
{
    public CharacterFaker()
    {
        RuleFor(c => c.UserId, f => f.Random.Int());
        RuleFor(c => c.Name, f => f.Person.FullName);
        RuleFor(c => c.CharacterConnectionsOne, []);
        RuleFor(c => c.CharacterConnectionsTwo, []);
    }
}
