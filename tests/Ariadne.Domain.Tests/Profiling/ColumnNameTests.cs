using Ariadne.Domain.Common;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class ColumnNameTests
{
    [Fact]
    public void CreateTrimsColumnName()
    {
        var name = new ColumnName("  SalePrice  ");

        Assert.Equal("SalePrice", name.Value);
    }

    [Fact]
    public void ColumnNamesCompareByExactOrdinalValue()
    {
        Assert.Equal(new ColumnName("SalePrice"), new ColumnName("SalePrice"));
        Assert.NotEqual(new ColumnName("SalePrice"), new ColumnName("saleprice"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateWithMissingValueThrowsDomainException(string? value)
    {
        Assert.Throws<DomainException>(() => new ColumnName(value!));
    }
}
