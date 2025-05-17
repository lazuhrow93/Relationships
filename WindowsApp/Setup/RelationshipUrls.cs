namespace WindowsApp.Setup;

public static class RelationshipUrls
{
    #region Paths
    
    public static string GetUserCharacters(int userId) => $"character/{userId}";
    public static string GetConnectionsForCharacter(int characterId) => $"character/connections/{characterId}";
    public static string GetDisconnectedForCharacter(int userId, int characterId) => $"character/nonconnections/{userId}/{characterId}";
    public static string CreateCharacter => $"character";
    public static string GetRelationTypes => $"platforminformation/relation-types";

    #endregion

    #region Query

    public static string WithConnections(bool value) => $"includeConnections={value}";

    #endregion
}
