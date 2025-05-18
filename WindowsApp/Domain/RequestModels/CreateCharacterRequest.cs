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

public class CreateConnectionRequest
{
    [JsonPropertyName("characterFrom")]
    public int CharacterFrom { get; set; }
    [JsonPropertyName("characterTo")]
    public int CharacterTo { get; set; }
    [JsonPropertyName("note")]
    public string? Note { get; set; }
    [JsonPropertyName("connectionTypeId")]
    public int ConnectionTypeId { get; set; }
}