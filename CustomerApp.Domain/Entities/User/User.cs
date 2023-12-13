using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Domain.Entities.User;

public class User
{
    public required Guid UserId { get; set; }
    [DataType(DataType.EmailAddress)]
    public required string UserEmail { get; set; }
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}
