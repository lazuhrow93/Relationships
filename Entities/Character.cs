namespace Entities;

public class Character : Entity
{
    public int UserId { get; set; }
    public string? Name { get; set; }

    public Connection[] StartConnections { get; set; } = [];

    public Connection[] EndConnections { get; set; } = [];
}
