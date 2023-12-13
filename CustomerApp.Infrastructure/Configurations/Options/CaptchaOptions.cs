namespace CustomerApp.Infrastructure.Configurations.Options;

public class CaptchaOptions
{
    public string SiteKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
#pragma warning disable CA1056 // URI-like properties should not be strings
    public string VerifyUrl { get; set; } = string.Empty;
#pragma warning restore CA1056 // URI-like properties should not be strings
}
