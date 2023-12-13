using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CustomerApp.Domain.Entities.User;
using CustomerApp.Application.Authentication;
using CustomerApp.Domain.Enums;
using CustomerApp.Infrastructure.Configurations.Options;
using System.Globalization;

namespace CustomerApp.Infrastructure.Authentication;

internal sealed class JwtProvider(IOptions<JwtOptions> jwtOptions,
    IPermissionService permissionService) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly IPermissionService _permissionService = permissionService;

    public async Task<string> Generate(User user)
    {
        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.UserEmail),
        };

        var _permissions = await _permissionService
            .GetPermissionsAsync(user.UserId)
            .ConfigureAwait(false);

        foreach (var item in _permissions)
        {
            claims.Add(new(Constants.Permissions, item));
        }

        var signingCredentials = new SigningCredentials(new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.IssuerSignKey!)),
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: null,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials);

        string tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}