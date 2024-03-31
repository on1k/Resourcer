using Resourcer.Data.DB.Models;
using Resourcer.Data.DTO;

namespace Resourcer.Helpers;

/// <summary>
/// Тут так же в угоду срок когда сделан маппер. Условно данные хранятся корректно
/// </summary>
public static class CustomMapper
{
    public static Response ToResponse(this Resource item) => new Response
    {
        Resource = item.Name,
        Decision = RequestValidator.ActionDictionary[item.Decision],
        Reason = item.Decision == "Grant" ? string.Empty : "Denied by user"
    };

}