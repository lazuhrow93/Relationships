namespace Api.Controllers.Dto;

public class GetConnectionsForCharacterResponse
{
    public int CharacterId { get; set; }
    public ConnectionDescriptionDto[] Connections { get; set; } = [];
}
