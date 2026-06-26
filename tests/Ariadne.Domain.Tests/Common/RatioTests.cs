using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;

namespace Ariadne.Domain.Tests.Common;

public class RatioTests
{
    public static TheoryData<double> InvalidRatioValues => new()
    {
        -0.001,
        1.001,
        double.NaN,
        double.PositiveInfinity,
        double.NegativeInfinity,
    };

    [Theory]
    [InlineData(0)]
    [InlineData(0.5)]
    [InlineData(1)]
    public void CreateWithValidValueStoresValue(double value)
    {
        var ratio = new Ratio(value);

        Assert.Equal(value, ratio.Value);
    }

    [Theory]
    [MemberData(nameof(InvalidRatioValues))]
    public void CreateWithInvalidValueThrowsDomainException(double value)
    {
        Assert.Throws<DomainException>(() => new Ratio(value));
    }

    [Fact]
    public void RatiosWithSameValueAreEqual()
    {
        Assert.Equal(new Ratio(0.25), new Ratio(0.25));
    }
}
