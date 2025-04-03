namespace Entities;

public class Character : Entity
{
    public string? Name { get; set; }

    public Connection[] Connections { get; set; } = [];
}
