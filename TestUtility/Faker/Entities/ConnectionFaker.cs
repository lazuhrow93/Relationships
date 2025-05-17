using Entities;
using Entities.Enums;

namespace TestUtility.Faker.Entities;

public class ConnectionFaker : EntityFaker<Connection, ConnectionFaker>
{
    public ConnectionFaker()
    {
        var characterFaker = new CharacterFaker();
        var char1 = characterFaker.Generate();
        var char2 = characterFaker.Generate();
        RuleFor(c => c.SourceCharacterId, _ => char1.Id);
        RuleFor(c => c.TargetCharacterId, _ => char2.Id);
        RuleFor(c => c.SourceCharacter, _ => char1);
        RuleFor(c => c.TargetCharacter, _ => char2);
        RuleFor(c => c.Notes, _ => []);
        RuleFor(c => c.RelationType, f => f.PickRandom<RelationType>());
    }
}
