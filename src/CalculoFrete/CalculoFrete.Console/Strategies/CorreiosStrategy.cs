using CalculoFrete.Console.Models;

namespace CalculoFrete.Console.Strategies;

public class CorreiosStrategy : IShippingStrategy
{
    public string Name => "Correios";

    public decimal CalculateShipping(ShippingInfo info)
    {
        decimal cost = 15.00m;
        cost += info.Weight * 2.50m;

        if (info.IsExpress)
            cost += 25.00m;

        if (info.Origin.Split('-')[1] == info.Destination.Split('-')[1])
            cost *= 0.85m;

        return cost;
    }

    public int GetDeliveryTime(ShippingInfo info)
        => info.IsExpress ? 3 : 7;

    public bool IsAvailable(ShippingInfo info)
        => true;
}