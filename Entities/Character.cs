using System.ComponentModel;

namespace Entities;

public class Character : Entity
{
    public int UserId { get; set; }
    public string? Name { get; set; }

    public List<Connection> CharacterConnectionsOne { get; set; } = [];

    public List<Connection> CharacterConnectionsTwo { get; set; } = [];
}
