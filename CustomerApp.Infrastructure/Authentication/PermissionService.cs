using CustomerApp.Application.Authentication;
using CustomerApp.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Infrastructure.Authentication;

public class PermissionService(ApplicationDbContext context) : IPermissionService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HashSet<string>> GetPermissionsAsync(Guid userId)
    {
        var roles = await _context.Set<User>()
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .Where(x => x.UserId == userId && x.IsActive)
            .Select(x => x.Roles)
            .ToArrayAsync()
            .ConfigureAwait(false);

        return roles.SelectMany(x => x)
            .SelectMany(x => x.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
