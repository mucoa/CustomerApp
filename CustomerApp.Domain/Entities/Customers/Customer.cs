using System.Text.Json.Serialization;

namespace CustomerApp.Domain.Entities.Customers;

public class Customer
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

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

    [JsonPropertyName("birthDate")]
    public required DateTime BirthDate { get; set; }

    [JsonPropertyName("company")]
    public string? Company { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
