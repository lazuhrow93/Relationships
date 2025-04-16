using Azure.Messaging.EventGrid.SystemEvents;

namespace Api.Dto;

public class CharacterDto
{
    public int UserId { get; set; }
    public string? Name { get; set; }
}
