using System.Text.Json.Serialization;

namespace WindowsApp.Domain.Models;

public class Connection
{
    [JsonPropertyName("targetCharacterId")]
    public int? CharacterId { get; set; }

    [JsonPropertyName("targetCharacter")]
    public string? Name { get; set; }

    [JsonPropertyName("connectionType")]
    public int? TypeOfConnection { get; set; }
}