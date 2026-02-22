using CalculoFrete.Console.Models;
using CalculoFrete.Console.Strategies;

namespace CalculoFrete.Tests;

public class FedExStrategyTests
{
    private readonly FedExStrategy _strategy;

    public FedExStrategyTests()
    {
        _strategy = new FedExStrategy();
    }

    [Fact]
    public void Name_ShouldReturnFedEx()
    {
        // Assert
        Assert.Equal("FedEx", _strategy.Name);
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
        // 30.00 + (10 * 5.00) = 80.00
        Assert.Equal(80.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldMultiplyByExpressFactor()
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
        // (30.00 + (10 * 5.00)) * 1.8 = 144.00
        Assert.Equal(144.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldAddFeeForNorteRegion()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Manaus-Norte",
            Weight = 10,
            IsExpress = false
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 30.00 + (10 * 5.00) + 20.00 = 100.00
        Assert.Equal(100.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldAddFeeForNordesteRegion()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Salvador-Nordeste",
            Weight = 10,
            IsExpress = false
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // 30.00 + (10 * 5.00) + 20.00 = 100.00
        Assert.Equal(100.00m, result);
    }

    [Fact]
    public void CalculateShipping_ShouldCombineExpressAndRegionFees()
    {
        // Arrange
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Fortaleza-Nordeste",
            Weight = 10,
            IsExpress = true
        };

        // Act
        var result = _strategy.CalculateShipping(info);

        // Assert
        // (30.00 + (10 * 5.00)) * 1.8 + 20.00 = 164.00
        Assert.Equal(164.00m, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldReturn2DaysForExpress()
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
        Assert.Equal(2, result);
    }

    [Fact]
    public void GetDeliveryTime_ShouldReturn5DaysForStandard()
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
        Assert.Equal(5, result);
    }
}

