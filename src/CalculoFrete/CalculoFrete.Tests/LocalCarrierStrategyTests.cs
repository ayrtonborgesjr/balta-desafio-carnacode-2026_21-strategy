using CalculoFrete.Console.Models;
using CalculoFrete.Console.Strategies;

namespace CalculoFrete.Tests;

public class LocalCarrierStrategyTests
{
    private readonly LocalCarrierStrategy _strategy;

    public LocalCarrierStrategyTests()
    {
        _strategy = new LocalCarrierStrategy();
    }

    [Fact]
    public void Name_ShouldReturnTransportadoraLocal()
    {
        // Assert
        Assert.Equal("Transportadora Local", _strategy.Name);
    }

    [Fact]
    public void IsAvailable_ShouldReturnTrueForSaoPaulo()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Campinas-São Paulo-SP",
            Weight = 10,
            IsExpress = false
        };

        // Act
        var result = _strategy.IsAvailable(info);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAvailable_ShouldReturnFalseForOtherStates()
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
        var result = _strategy.IsAvailable(info);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalculateShipping_ShouldCalculateCostForSaoPaulo()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Campinas-São Paulo-SP",
            Weight = 10,
            IsExpress = false
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 8.00 + (10 * 1.50) = 23.00
        Assert.Equal(23.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldReturn0ForOtherStates()
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
        var result = _strategy.CalculateShipping(info);

        // Assert
        Assert.Equal(0m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldCalculateForDifferentWeights()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Santos-São Paulo-SP",
            Weight = 25,
            IsExpress = false
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 8.00 + (25 * 1.50) = 45.50
        Assert.Equal(45.50m, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldAlwaysReturn1Day()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Campinas-São Paulo-SP",
            Weight = 10,
            IsExpress = false
        };

        // Act
        var result = _strategy.GetDeliveryTime(info);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldReturn1DayEvenForExpress()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Campinas-São Paulo-SP",
            Weight = 10,
            IsExpress = true
        };

        // Act
        var result = _strategy.GetDeliveryTime(info);

        // Assert
        Assert.Equal(1, result);
    }
}

