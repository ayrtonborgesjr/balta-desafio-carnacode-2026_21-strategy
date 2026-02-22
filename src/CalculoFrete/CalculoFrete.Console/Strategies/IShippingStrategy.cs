using CalculoFrete.Console.Models;

namespace CalculoFrete.Console.Strategies;

public interface IShippingStrategy
{
    decimal CalculateShipping(ShippingInfo info);
    int GetDeliveryTime(ShippingInfo info);
    bool IsAvailable(ShippingInfo info);
    string Name { get; }
}