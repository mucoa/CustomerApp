using System.Text.Json.Serialization;

namespace CustomerApp.Presentation.Requests.CustomerEndpointRequests;

public class SaveCustomerRequest
{
    [JsonPropertyName("identity")]
    public required string Identity { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("phoneNumber")]
    public required string PhoneNumber { get; set; }

    [JsonPropertyName("emailAddress")]
    public required string EmailAddress { get; set; }

    [JsonPropertyName("address")]
    public required string Address { get; set; }

    [JsonPropertyName("birthdate")]
    public required DateTime Birthdate { get; set; }

    [JsonPropertyName("company")]
    public required string Company { get; set; }

}
