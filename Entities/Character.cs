namespace Entities;

public class Character : Entity
{
    public int UserId { get; set; }
    public string? Name { get; set; }

    public Connection[] CharacterConnectionsOne { get; set; } = [];

    public Connection[] CharacterConnectionsTwo { get; set; } = [];
}
