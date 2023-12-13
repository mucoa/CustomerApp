using CustomerApp.Infrastructure.Configurations.Options;
using Microsoft.Extensions.Options;

namespace CustomerApp.WebAPI.OptionsSetup;

public sealed class DatabaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOption>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly string SectionName = "DatabaseOptions";

    public void Configure(DatabaseOption options)
    {
        string connectionString = _configuration.GetConnectionString("Database") ?? string.Empty;

        if (options is not null)
        {
            options.ConnectionString = connectionString;
        }

        _configuration.GetSection(SectionName).Bind(options);
    }
}
