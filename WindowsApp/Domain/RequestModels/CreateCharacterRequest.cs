using System.Text.Json.Serialization;

namespace WindowsApp.Domain.RequestModels;

public class CreateCharacterRequest
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("characterName")]
    public string? CharacterName { get; set; }

    [JsonPropertyName("roleToCharacter")]
    public string? RoleToCharacter { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}
