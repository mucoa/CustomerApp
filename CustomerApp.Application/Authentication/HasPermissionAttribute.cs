using CustomerApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace CustomerApp.Application.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permissions permission)
        : base(policy: permission.ToString())
    {

    }
}
