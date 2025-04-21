namespace Entities;

public class ConnectionNote : Entity
{
    public string? Content { get; set; }
    public int ConnectionId { get; set; }
    public Connection? Connection { get; set; }
}
 