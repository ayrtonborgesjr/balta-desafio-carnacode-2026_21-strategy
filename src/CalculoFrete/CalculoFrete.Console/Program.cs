using CalculoFrete.Console.Context;
using CalculoFrete.Console.Models;
using CalculoFrete.Console.Strategies;

var shipping = new ShippingInfo
{
    Origin = "São Paulo-SP",
    Destination = "Rio de Janeiro-RJ",
    Weight = 5.0m,
    IsExpress = false
};

var calculator = new ShippingCalculator();

var strategies = new IShippingStrategy[]
{
    new CorreiosStrategy(),
    new FedExStrategy(),
    new DHLStrategy(),
    new LocalCarrierStrategy()
};

Console.WriteLine("=== Comparando Transportadoras ===");

foreach (var strategy in strategies)
{
    calculator.SetStrategy(strategy);
    calculator.Calculate(shipping);
}