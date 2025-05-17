using System.Text.Json.Serialization;

namespace WindowsApp.Domain.Models;

public class RelationTypes
{
    [JsonPropertyName("types")]
    public Dto[] Types { get; set; } = [];

    public class Dto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
