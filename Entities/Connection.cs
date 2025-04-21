using System.Diagnostics.CodeAnalysis;
using Entities.Enums;

namespace Entities;

public class Connection : Entity
{
    public int SourceCharacterId { get; set; }

    public int TargetCharacterId { get; set; }

    [AllowNull]
    public Character SourceCharacter { get; set; }

    [AllowNull]
    public Character TargetCharacter { get; set; }

    public List<ConnectionNote> Notes { get; set; } = [];

    public ConnectionType ConnectionType { get; set; }
}
