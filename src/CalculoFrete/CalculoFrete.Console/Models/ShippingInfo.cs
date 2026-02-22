namespace CalculoFrete.Console.Models;

public class ShippingInfo
{
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public decimal Weight { get; set; }
    public bool IsExpress { get; set; }
}