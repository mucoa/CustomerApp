using CustomerApp.Domain.Entities.Customers;
using System.Text.Json.Serialization;

namespace CustomerApp.Presentation.Requests.CustomerEndpointRequests;

public class UpdateCustomerRequest
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }
    
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

    [JsonPropertyName("orders")]
    public ICollection<Order>? Orders { get; init; }
}
