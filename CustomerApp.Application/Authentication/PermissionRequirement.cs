﻿using Microsoft.AspNetCore.Authorization;

namespace CustomerApp.Application.Authentication;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
