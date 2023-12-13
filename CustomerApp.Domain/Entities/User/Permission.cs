namespace CustomerApp.Domain.Entities.User;

public class Permission
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public bool IsDisabled { get; init; } = false;
}
