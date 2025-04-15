using System.Diagnostics.CodeAnalysis;
using Entities.Enums;

namespace Entities;

public class Connection : Entity
{
    public int CharacterOneId { get; set; }

    public int CharacterTwoId { get; set; }

    [AllowNull]
    public Character CharacterOne { get; set; }

    [AllowNull]
    public Character CharacterTwo { get; set; }

    public ConnectionType ConnectionType { get; set; }
}
