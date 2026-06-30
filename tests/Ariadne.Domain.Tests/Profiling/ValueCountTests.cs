using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class ValueCountTests
{
    [Fact]
    public void CreateStoresValueCountMetadata()
    {
        ValueCount valueCount = ValueCount.Create("A", 3, 0.75);

        Assert.Equal("A", valueCount.DisplayValue);
        Assert.Equal(3, valueCount.Count);
        Assert.Equal(0.75, valueCount.Ratio.Value);
    }

    [Fact]
    public void CreateAllowsEmptyDisplayValue()
    {
        var valueCount = new ValueCount(string.Empty, 1, Ratio.One);

        Assert.Equal(string.Empty, valueCount.DisplayValue);
    }

    [Fact]
    public void CreateWithNullDisplayValueThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new ValueCount(null!, 1, Ratio.One));
    }

    [Fact]
    public void CreateWithNegativeCountThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new ValueCount("A", -1, Ratio.One));
    }

    [Fact]
    public void CreateWithInvalidRatioThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => ValueCount.Create("A", 1, 1.1));
    }
}
