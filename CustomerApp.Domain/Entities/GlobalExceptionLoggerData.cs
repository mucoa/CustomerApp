using System.Net;

namespace CustomerApp.Domain.Entities;

public class GlobalExceptionLoggerData
{
    public string? Message { get; set; }
    public object? Request { get; set; }
    public IPAddress? IpAddress { get; set; }
    public DateTime? Date { get; set; }
}
