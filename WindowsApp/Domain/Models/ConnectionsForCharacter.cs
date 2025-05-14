using System.Text.Json.Serialization;

namespace WindowsApp.Domain.Models;

public class ConnectionsForCharacter
{
    [JsonPropertyName("characterId")]
    public int CharacterId { get; set; }

    [JsonPropertyName("connections")]
    public Dto[] Connections { get; set; } = [];

    public class Dto
    {
        [JsonPropertyName("id")]
        public int CharacterId { get; set; }

        [JsonPropertyName("characterName")]
        public string? CharacterName { get; set; }

        [JsonPropertyName("roleToCharacter")]
        public string? ConnectionDescription { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}
