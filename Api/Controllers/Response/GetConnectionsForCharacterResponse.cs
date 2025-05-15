using Api.Controllers.Dto;

namespace Api.Controllers.Response;

public class GetConnectionsForCharacterResponse
{
    public int CharacterId { get; set; }
    public ConnectionDescriptionDto[] Connections { get; set; } = [];
}
