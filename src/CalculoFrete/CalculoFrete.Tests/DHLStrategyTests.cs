using CalculoFrete.Console.Models;
using CalculoFrete.Console.Strategies;

namespace CalculoFrete.Tests;

public class DHLStrategyTests
{
    private readonly DHLStrategy _strategy;

    public DHLStrategyTests()
    {
        _strategy = new DHLStrategy();
    }

    [Fact]
    public void Name_ShouldReturnDHL()
    {
        // Assert
        Assert.Equal("DHL", _strategy.Name);
    }

    [Fact]
    public void IsAvailable_ShouldReturnTrueWhenWeightIs50OrLess()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 50,
            IsExpress = false
        };

        // Act
        var result = _strategy.IsAvailable(info);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAvailable_ShouldReturnFalseWhenWeightExceeds50()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 51,
            IsExpress = false
        };

        // Act
        var result = _strategy.IsAvailable(info);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CalculateShipping_ShouldCalculateBasicCostForLightWeight()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 5,
            IsExpress = false
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 25.00 + (5 * 4.50) = 47.50
        Assert.Equal(47.50m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldAddExtraFeeForWeightOver10()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 15,
            IsExpress = false
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 25.00 + (15 * 4.50) + ((15 - 10) * 2.00) = 102.50
        Assert.Equal(102.50m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldAddExpressFee()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 5,
            IsExpress = true
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 25.00 + (5 * 4.50) + 35.00 = 82.50
        Assert.Equal(82.50m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldCombineAllFees()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 20,
            IsExpress = true
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 25.00 + (20 * 4.50) + ((20 - 10) * 2.00) + 35.00 = 170.00
        Assert.Equal(170.00m, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldReturn1DayForExpress()
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
        Assert.Equal(1, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldReturn4DaysForStandard()
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
        Assert.Equal(4, result);
    }
}

