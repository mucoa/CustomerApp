namespace CustomerApp.Infrastructure.Configurations.Options;

public class DatabaseOption
{
    public string ConnectionString { get; set; } = string.Empty;
    public string ProgramConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; }
    public int CommandTimeout { get; set; }
    public bool EnabledDetailErrors { get; set; }
    public bool EnabledSensetiveDataLogging { get; set; }
}
