using System.Text.Json.Serialization;

namespace WindowsApp.Domain.Models;

public class Character
{
    [JsonPropertyName("userId")]
    public int? UserId { get; set; }

    [JsonPropertyName("name")]
    public string? CharacterName { get; set; }

    [JsonPropertyName("targetConnections")]
    public Connection[]? Connections { get; set; }

    public int TotalConnections => Connections?.Count() ?? 0;
}
