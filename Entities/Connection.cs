using System.Diagnostics.CodeAnalysis;

namespace Entities;

public class Connection : Entity
{
    public int SourceCharacterId { get; set; }

    public int TargetCharacterId { get; set; }
    
    public int RelationTypeId { get; set; }

    [AllowNull]
    public Character SourceCharacter { get; set; }

    [AllowNull]
    public Character TargetCharacter { get; set; }

    public List<ConnectionNote> Notes { get; set; } = [];

    public RelationType? RelationType { get; set; }
}
