namespace CustomerApp.Domain.Results.Customer;

public class GetOrderResult
{
    public int OrderNumber { get; set; }
    public string? Product { get; set; }
    public double? Price { get; set; }
    public string? CustomerName { get; set; }
    public DateTime Date { get; set; }
}
