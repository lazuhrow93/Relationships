﻿namespace Api.Controllers.Response;

public class ConnectCharactersResponse
{
    public int ConnectionId { get; set; }
    public int CharacterOneId { get; set; }
    public int CharacterTwoId { get; set; }
    public int ConnectionTypeId { get; set; }
}
