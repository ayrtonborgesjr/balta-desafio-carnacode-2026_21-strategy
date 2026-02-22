using CalculoFrete.Console.Models;

namespace CalculoFrete.Console.Strategies;

public class DHLStrategy : IShippingStrategy
{
    public string Name => "DHL";

    public decimal CalculateShipping(ShippingInfo info)
    {
        decimal cost = 25.00m;
        cost += info.Weight * 4.50m;

        if (info.Weight > 10)
            cost += (info.Weight - 10) * 2.00m;

        if (info.IsExpress)
            cost += 35.00m;

        return cost;
    }

    public int GetDeliveryTime(ShippingInfo info)
        => info.IsExpress ? 1 : 4;

    public bool IsAvailable(ShippingInfo info)
        => info.Weight <= 50;
}