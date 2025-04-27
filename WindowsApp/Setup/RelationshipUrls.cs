namespace WindowsApp.Setup;

public static class RelationshipUrls
{
    #region Paths
    
    public static string GetUserCharacters(int userId) => $"character/{userId}";

    #endregion

    #region Query

    public static string WithConnections(bool value) => $"includeConnections={value}";

    #endregion
}
