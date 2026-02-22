using CalculoFrete.Console.Models;
using CalculoFrete.Console.Strategies;

namespace CalculoFrete.Tests;

public class CorreiosStrategyTests
{
    private readonly CorreiosStrategy _strategy;

    public CorreiosStrategyTests()
    {
        _strategy = new CorreiosStrategy();
    }

    [Fact]
    public void Name_ShouldReturnCorreios()
    {
        // Assert
        Assert.Equal("Correios", _strategy.Name);
    }

    [Fact]
    public void IsAvailable_ShouldAlwaysReturnTrue()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 100,
            IsExpress = false
        };

        // Act
        var result = _strategy.IsAvailable(info);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalculateShipping_ShouldCalculateBasicCost()
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
        // 15.00 + (10 * 2.50) = 40.00
        Assert.Equal(40.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldAddExpressFee()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = true
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 15.00 + (10 * 2.50) + 25.00 = 65.00
        Assert.Equal(65.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldApplyDiscountForSameState()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Campinas-SP",
            Weight = 10,
            IsExpress = false
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // (15.00 + (10 * 2.50)) * 0.85 = 34.00
        Assert.Equal(34.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldApplyDiscountAndExpressFee()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Campinas-SP",
            Weight = 10,
            IsExpress = true
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // (15.00 + (10 * 2.50) + 25.00) * 0.85 = 55.25
        Assert.Equal(55.25m, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldReturn3DaysForExpress()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10,
            IsExpress = true
        };

        // Act
        var result = _strategy.GetDeliveryTime(info);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldReturn7DaysForStandard()
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
        var result = _strategy.GetDeliveryTime(info);

        // Assert
        Assert.Equal(7, result);
    }
}

