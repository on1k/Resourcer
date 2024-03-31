using Resourcer.Data.DB.Models;

namespace Resourcer.Test.Helpers;

public static class DataBaseProvider
{
    public static List<Resource> GetResources()
    {
        return new List<Resource>
        {
            new Resource { Decision = "Deny", Name = "res1" },
            new Resource { Decision = "Grant", Name = "res2" }
        };
    }
}