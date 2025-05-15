using Api.Controllers.Dto;

namespace Api.Controllers.Response;

public class GetDisconnectedCharactersResponse
{
    public DisconnectedCharactersDto[] Disconnections { get; set; } = [];
}
