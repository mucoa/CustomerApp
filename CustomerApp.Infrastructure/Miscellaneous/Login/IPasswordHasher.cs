using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Infrastructure.Miscellaneous.Login;

public interface IPasswordHasher
{
    string Hash([DataType(DataType.Password)] string Password);
    bool Verify([DataType(DataType.Password)] string Password, [DataType(DataType.Password)] string HashedPassword);
}
