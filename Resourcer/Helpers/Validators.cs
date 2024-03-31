using Resourcer.Data.DTO;

namespace Resourcer.Helpers;
/// <summary>
/// Валидаторы сделал отдельно методом в угоду сокращения количества строк, которые рекомендуют в задаче.
/// По хорошему конечно валидацию стоит сделать в отдельный слой и вызывать не в контроллере.
/// И конечно можно было бы сделать базовый валидатор Request(так как повторяющиеся поля для проверки встречаются), а от него уже
/// наследоваться в более сложные валидаторы. Но мы условимся чисто на статических классах
/// </summary>
public static class AccessValidator
{
    private static List<string> _validActions = new List<string>
    {
        "Grant",
        "Deny"
    };
    
    public static bool IsValid(this SetAccess item)
    {
        if (string.IsNullOrEmpty(item.Action) ||
            !_validActions.Contains(item.Action)) return false;
        if (string.IsNullOrEmpty(item.Resource)) return false;
        return true;
    }
}

public static class RequestValidator
{
    public static Dictionary<string, string> ActionDictionary = new Dictionary<string, string>
    {
        { "Grant", "Granted" },
        { "Deny", "Denied" }
    };
    public static bool IsValid(this Request item) => !string.IsNullOrEmpty(item.Resource);
}
    
