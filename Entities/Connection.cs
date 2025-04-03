using System.Diagnostics.CodeAnalysis;
using Entities.Enums;

namespace Entities;

public class Connection : Entity
{
    public int CharacterStartId { get; set; }

    public int CharacterEndId { get; set; }

    [AllowNull]
    public Character CharacterStart { get; set; }

    [AllowNull]
    public Character CharacterEnd { get; set; }

    public ConnectionType ConnectionType { get; set; }
}
