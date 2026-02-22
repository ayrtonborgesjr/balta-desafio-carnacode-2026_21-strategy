using CalculoFrete.Console.Models;
using CalculoFrete.Console.Strategies;

namespace CalculoFrete.Console.Context;

public class ShippingCalculator
{
    private IShippingStrategy? _strategy;

    public void SetStrategy(IShippingStrategy strategy)
    {
        _strategy = strategy;
    }

    public void Calculate(ShippingInfo info)
    {
        if (_strategy == null)
        {
            System.Console.WriteLine("❌ Nenhuma estratégia definida.");
            return;
        }

        if (!_strategy.IsAvailable(info))
        {
            System.Console.WriteLine($"❌ {_strategy.Name} não disponível.");
            return;
        }

        var cost = _strategy.CalculateShipping(info);
        var time = _strategy.GetDeliveryTime(info);

        System.Console.WriteLine($"\n=== {_strategy.Name} ===");
        System.Console.WriteLine($"Frete: R$ {cost:N2}");
        System.Console.WriteLine($"Prazo: {time} dias úteis");
    }
}