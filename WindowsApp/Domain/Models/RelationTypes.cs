using System.Text.Json.Serialization;

namespace WindowsApp.Domain.Models;

public class RelationTypes
{
    [JsonPropertyName("types")]
    public Dto[] Types { get; set; } = [];

    public class Dto
    {
        [JsonPropertyName("relationTypeId")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
