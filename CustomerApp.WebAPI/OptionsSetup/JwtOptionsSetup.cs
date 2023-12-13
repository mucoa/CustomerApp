using CustomerApp.Infrastructure.Configurations.Options;
using Microsoft.Extensions.Options;

namespace CustomerApp.WebAPI.OptionsSetup;

public sealed class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly string SectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
