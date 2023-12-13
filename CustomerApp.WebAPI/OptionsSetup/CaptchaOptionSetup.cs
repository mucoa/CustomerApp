using CustomerApp.Infrastructure.Configurations.Options;
using Microsoft.Extensions.Options;

namespace CustomerApp.WebAPI.OptionsSetup;

public sealed class CaptchaOptionSetup(IConfiguration configuration) : IConfigureOptions<CaptchaOptions>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly string SectionName = "Captcha";

    public void Configure(CaptchaOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
