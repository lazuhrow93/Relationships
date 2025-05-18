namespace Entities;

public class Character : Entity
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public List<Connection> SourceConnections { get; set; } = []; //connections where THIS character is the source

    public List<Connection> TargetConnections { get; set; } = []; //connections where THIS character is the target
}
