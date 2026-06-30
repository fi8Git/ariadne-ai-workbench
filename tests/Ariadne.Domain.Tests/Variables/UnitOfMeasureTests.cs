using Ariadne.Domain.Common;
using Ariadne.Domain.Variables;

namespace Ariadne.Domain.Tests.Variables;

public class UnitOfMeasureTests
{
    [Fact]
    public void CreateTrimsUnitOfMeasure()
    {
        var unit = new UnitOfMeasure("  kg  ");

        Assert.Equal("kg", unit.Value);
    }

    [Fact]
    public void UnitsWithSameValueAreEqual()
    {
        Assert.Equal(new UnitOfMeasure("m2"), new UnitOfMeasure("m2"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateWithMissingValueThrowsDomainException(string? value)
    {
        Assert.Throws<DomainException>(() => new UnitOfMeasure(value!));
    }

    [Fact]
    public void CreateWithTooLongValueThrowsDomainException()
    {
        string value = new('m', UnitOfMeasure.MaxLength + 1);

        Assert.Throws<DomainException>(() => new UnitOfMeasure(value));
    }
}
