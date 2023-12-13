using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CustomerApp.Presentation.Requests.UserEndpointRequests;

public class CreateRequest
{
    [JsonPropertyName("email")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [JsonPropertyName("password")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
