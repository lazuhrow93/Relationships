namespace Api.Controllers.Dto;

public class GetConnectionsForCharacterResponse
{
    public int CharacterId { get; set; }
    public int[] ConnectionIds { get; set; } = [];
}
