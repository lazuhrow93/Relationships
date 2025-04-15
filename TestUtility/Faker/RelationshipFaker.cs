using Bogus;

namespace TestUtility.Faker;

public class RelationshipFaker<T, TFaker> : Faker<T>
    where T : class
    where TFaker : RelationshipFaker<T, TFaker>
{
    public RelationshipFaker()
    {
        StrictMode(true);
        UseSeed(453495671);
    }
}
