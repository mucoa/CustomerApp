using System.Text.Json.Serialization;

namespace CustomerApp.Domain.Entities.Customers;

public class Order
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("orderNumber")]
    public int OrderNumber { get; set; }

    [JsonPropertyName("product")]
    public required string Product { get; set; }

    [JsonPropertyName("productPrice")]
    public required double ProductPrice { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    public required Customer Customer { get; set; }
}
