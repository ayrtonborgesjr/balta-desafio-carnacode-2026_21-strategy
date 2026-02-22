using CalculoFrete.Console.Context;
using CalculoFrete.Console.Models;
using CalculoFrete.Console.Strategies;

namespace CalculoFrete.Tests;

public class ShippingCalculatorTests : IDisposable
{
    private readonly ShippingCalculator _calculator;
    private readonly StringWriter _stringWriter;
    private readonly TextWriter _originalOutput;

    public ShippingCalculatorTests()
    {
        _calculator = new ShippingCalculator();
        _stringWriter = new StringWriter();
        _originalOutput = System.Console.Out;
        System.Console.SetOut(_stringWriter);
    }

    public void Dispose()
    {
        System.Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }

    [Fact]
    public void SetStrategy_ShouldSetStrategySuccessfully()
    {
        // Arrange
        var strategy = new CorreiosStrategy();

        // Act
        _calculator.SetStrategy(strategy);

        // Assert - No exception means success
        Assert.True(true);
    }

    [Fact]
    public void Calculate_ShouldPrintErrorWhenNoStrategySet()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = false
        };

        // Act
        _calculator.Calculate(info);
        var output = _stringWriter.ToString();

        // Assert
        Assert.Contains("❌ Nenhuma estratégia definida.", output);
    }

    [Fact]
    public void Calculate_ShouldPrintErrorWhenStrategyNotAvailable()
    {
        // Arrange
        var strategy = new LocalCarrierStrategy();
        _calculator.SetStrategy(strategy);
        
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = false
        };

        // Act
        _calculator.Calculate(info);
        var output = _stringWriter.ToString();

        // Assert
        Assert.Contains("❌ Transportadora Local não disponível.", output);
    }

    [Fact]
    public void Calculate_ShouldPrintCorrectInformationForCorreios()
    {
        // Arrange
        var strategy = new CorreiosStrategy();
        _calculator.SetStrategy(strategy);
        
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = false
        };

        // Act
        _calculator.Calculate(info);
        var output = _stringWriter.ToString();

        // Assert
        Assert.Contains("=== Correios ===", output);
        Assert.Contains("Frete: R$ 40,00", output);
        Assert.Contains("Prazo: 7 dias úteis", output);
    }

    [Fact]
    public void Calculate_ShouldPrintCorrectInformationForFedEx()
    {
        // Arrange
        var strategy = new FedExStrategy();
        _calculator.SetStrategy(strategy);
        
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = false
        };

        // Act
        _calculator.Calculate(info);
        var output = _stringWriter.ToString();

        // Assert
        Assert.Contains("=== FedEx ===", output);
        Assert.Contains("Frete: R$ 80,00", output);
        Assert.Contains("Prazo: 5 dias úteis", output);
    }

    [Fact]
    public void Calculate_ShouldPrintCorrectInformationForDHL()
    {
        // Arrange
        var strategy = new DHLStrategy();
        _calculator.SetStrategy(strategy);
        
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = true
        };

        // Act
        _calculator.Calculate(info);
        var output = _stringWriter.ToString();

        // Assert
        Assert.Contains("=== DHL ===", output);
        Assert.Contains("Frete: R$ 105,00", output);
        Assert.Contains("Prazo: 1 dias úteis", output);
    }

    [Fact]
    public void Calculate_ShouldPrintCorrectInformationForLocalCarrier()
    {
        // Arrange
        var strategy = new LocalCarrierStrategy();
        _calculator.SetStrategy(strategy);
        
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Campinas-São Paulo-SP",
            Weight = 10,
            IsExpress = false
        };

        // Act
        _calculator.Calculate(info);
        var output = _stringWriter.ToString();

        // Assert
        Assert.Contains("=== Transportadora Local ===", output);
        Assert.Contains("Frete: R$ 23,00", output);
        Assert.Contains("Prazo: 1 dias úteis", output);
    }

    [Fact]
    public void Calculate_ShouldAllowChangingStrategy()
    {
        // Arrange
        var correios = new CorreiosStrategy();
        var fedex = new FedExStrategy();
        
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = false
        };

        // Act
        _calculator.SetStrategy(correios);
        _calculator.Calculate(info);
        var firstOutput = _stringWriter.ToString();

        _stringWriter.GetStringBuilder().Clear();

        _calculator.SetStrategy(fedex);
        _calculator.Calculate(info);
        var secondOutput = _stringWriter.ToString();

        // Assert
        Assert.Contains("=== Correios ===", firstOutput);
        Assert.Contains("Frete: R$ 40,00", firstOutput);
        
        Assert.Contains("=== FedEx ===", secondOutput);
        Assert.Contains("Frete: R$ 80,00", secondOutput);
    }

    [Fact]
    public void Calculate_ShouldHandleFedExWeightLimit()
    {
        // Arrange
        var strategy = new FedExStrategy();
        _calculator.SetStrategy(strategy);
        
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 51,
            IsExpress = false
        };

        // Act
        _calculator.Calculate(info);
        var output = _stringWriter.ToString();

        // Assert
        Assert.Contains("❌ FedEx não disponível.", output);
    }
}

