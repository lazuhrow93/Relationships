using System.Text.Json.Serialization;

namespace WindowsApp.Domain.Models;

public class Connection
{
    [JsonPropertyName("characterId")]
    public int? CharacterId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("typeOfConnection")]
    public string? TypeOfConnection { get; set; }
}