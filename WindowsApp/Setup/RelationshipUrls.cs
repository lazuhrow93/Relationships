namespace WindowsApp.Setup;

public static class RelationshipUrls
{
    public static string GetUserCharacters(int userId) => $"/character/{userId}";
}
