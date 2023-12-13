namespace CustomerApp.Infrastructure.Configurations.Options;
public class JwtOptions
{
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
    public string? IssuerSignKey { get; set; }
}