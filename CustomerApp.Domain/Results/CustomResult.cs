using System.Text.Json.Serialization;

namespace CustomerApp.Presentation.Results;

public sealed class CustomResult
{
    [JsonPropertyName("isSucceed")]
    public required bool IsSucceed { get; set; }
    [JsonPropertyName("result")]
    public object? Result { get; set; }
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }
}
