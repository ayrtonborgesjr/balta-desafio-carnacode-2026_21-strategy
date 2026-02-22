using CalculoFrete.Console.Models;

namespace CalculoFrete.Console.Strategies;

public class FedExStrategy : IShippingStrategy
{
    public string Name => "FedEx";

    public decimal CalculateShipping(ShippingInfo info)
    {
        decimal cost = 30.00m;
        cost += info.Weight * 5.00m;

        if (info.IsExpress)
            cost *= 1.8m;

        if (info.Destination.Contains("Norte") ||
            info.Destination.Contains("Nordeste"))
            cost += 20.00m;

        return cost;
    }

    public int GetDeliveryTime(ShippingInfo info)
        => info.IsExpress ? 2 : 5;

    public bool IsAvailable(ShippingInfo info)
        => info.Weight <= 50;
}