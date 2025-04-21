using System.ComponentModel;

namespace Entities;

public class Character : Entity
{
    public int UserId { get; set; }
    public string? Name { get; set; }

    public List<Connection> SourceConnections { get; set; } = [];

    public List<Connection> TargetConnections { get; set; } = [];
}
