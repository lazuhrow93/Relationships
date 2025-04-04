namespace Entities;

public class Character : Entity
{
    public int UserId { get; set; }
    public string? Name { get; set; }

    public Connection[] Connections { get; set; } = [];
}
