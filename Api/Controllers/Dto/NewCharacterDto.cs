using Azure.Messaging.EventGrid.SystemEvents;

namespace Api.Controllers.Dto;

public class NewCharacterDto
{
    public int Id { get; set; }
    public string? CharacterName { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
}
