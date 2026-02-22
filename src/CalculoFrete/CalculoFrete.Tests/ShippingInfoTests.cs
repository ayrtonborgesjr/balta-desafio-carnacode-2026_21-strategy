using CalculoFrete.Console.Models;

namespace CalculoFrete.Tests;

public class ShippingInfoTests
{
    [Fact]
    public void Constructor_ShouldCreateInstanceWithRequiredProperties()
    {
        // Arrange & Act
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10.5m,
            IsExpress = true
        };

        // Assert
        Assert.Equal("São Paulo-SP", info.Origin);
        Assert.Equal("Rio de Janeiro-RJ", info.Destination);
        Assert.Equal(10.5m, info.Weight);
        Assert.True(info.IsExpress);
    }

    [Fact]
    public void Properties_ShouldBeSettable()
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
        info.Origin = "Campinas-SP";
        info.Destination = "Santos-SP";
        info.Weight = 25.5m;
        info.IsExpress = true;

        // Assert
        Assert.Equal("Campinas-SP", info.Origin);
        Assert.Equal("Santos-SP", info.Destination);
        Assert.Equal(25.5m, info.Weight);
        Assert.True(info.IsExpress);
    }

    [Fact]
    public void IsExpress_ShouldDefaultToFalse()
    {
        // Arrange & Act
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10
        };

        // Assert
        Assert.False(info.IsExpress);
    }

    [Fact]
    public void Weight_ShouldAcceptDecimalValues()
    {
        // Arrange & Act
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 10.75m,
            IsExpress = false
        };

        // Assert
        Assert.Equal(10.75m, info.Weight);
    }

    [Fact]
    public void Weight_ShouldAcceptZero()
    {
        // Arrange & Act
        var info = new ShippingInfo
        {
            Origin = "São Paulo-SP",
            Destination = "Rio de Janeiro-RJ",
            Weight = 0,
            IsExpress = false
        };

        // Assert
        Assert.Equal(0m, info.Weight);
    }
}

