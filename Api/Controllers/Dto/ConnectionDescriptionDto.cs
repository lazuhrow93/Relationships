using Azure.Messaging.EventGrid.SystemEvents;

namespace Api.Controllers.Dto;

public class ConnectionDescriptionDto
{
    public int Id { get; set; }
    public string? CharacterName { get; set; }
    public string? RoleToCharacter { get; set; }
    public int UserId { get; set; }
}
