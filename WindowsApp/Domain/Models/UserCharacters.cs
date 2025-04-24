using System.Text.Json.Serialization;

namespace WindowsApp.Domain.Models;

public class UserCharacters
{
    [JsonPropertyName("userId")]
    public string? UserId { get; set; }

    [JsonPropertyName("name")]
    public string? CharacterName { get; set; }
}
