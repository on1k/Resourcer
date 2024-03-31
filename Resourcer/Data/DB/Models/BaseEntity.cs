namespace Resourcer.Data.DB.Models;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}