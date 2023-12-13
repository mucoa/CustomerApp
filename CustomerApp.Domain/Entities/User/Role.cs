using CustomerApp.Domain.Enums;

namespace CustomerApp.Domain.Entities.User;

public sealed class Role(int id, string name) : Enumeration<Role>(id, name)
{
    public static readonly Role Administrator = new(1, nameof(Administrator));
    public static readonly Role StandartUser = new(2, nameof(StandartUser));

    public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
    public ICollection<User> Users { get; init; }= new List<User>();
    public bool IsDisabled { get; init; }
}
