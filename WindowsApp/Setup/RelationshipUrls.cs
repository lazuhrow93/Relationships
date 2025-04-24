using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApp.Setup;

public static class RelationshipUrls
{
    public static string GetUserCharacters(int userId) => $"/api/relationship/mycharacters/{userId}";
}
