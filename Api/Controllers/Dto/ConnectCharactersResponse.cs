using Entities.Enums;

namespace Api.Controllers.Dto;

public class ConnectCharactersResponse
{
    public int ConnectionId { get; set; }
    public int CharacterOneId { get; set; }
    public int CharacterTwoId { get; set; }
    public int ConnectionTypeId { get; set; }
}
