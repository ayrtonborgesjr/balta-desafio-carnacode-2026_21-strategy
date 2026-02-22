using CalculoFrete.Console.Models;

namespace CalculoFrete.Console.Strategies;

public class LocalCarrierStrategy : IShippingStrategy
{
    public string Name => "Transportadora Local";

    public decimal CalculateShipping(ShippingInfo info)
    {
        if (!info.Destination.Contains("São Paulo-SP"))
            return 0;

        decimal cost = 8.00m;
        cost += info.Weight * 1.50m;

        return cost;
    }

    public int GetDeliveryTime(ShippingInfo info)
        => 1;

    public bool IsAvailable(ShippingInfo info)
        => info.Destination.Contains("São Paulo-SP");
}