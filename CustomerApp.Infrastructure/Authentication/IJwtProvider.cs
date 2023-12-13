using CustomerApp.Domain.Entities.User;

namespace CustomerApp.Infrastructure.Authentication;

public interface IJwtProvider
{
    Task<string> Generate(User user);
}
