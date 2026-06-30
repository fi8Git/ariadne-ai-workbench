using Ariadne.Domain.Common;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class NumericColumnStatisticsTests
{
    [Fact]
    public void CreateWithValidValuesStoresStatistics()
    {
        var statistics = new NumericColumnStatistics(
            minimum: 1,
            maximum: 10,
            mean: 5,
            median: 6,
            q1: 3,
            q3: 8,
            standardDeviation: 2);

        Assert.Equal(1, statistics.Minimum);
        Assert.Equal(10, statistics.Maximum);
        Assert.Equal(5, statistics.Mean);
        Assert.Equal(6, statistics.Median);
        Assert.Equal(3, statistics.Q1);
        Assert.Equal(8, statistics.Q3);
        Assert.Equal(2, statistics.StandardDeviation);
    }

    [Fact]
    public void CreateWithMinimumGreaterThanMaximumThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new NumericColumnStatistics(minimum: 10, maximum: 1));
    }

    [Fact]
    public void CreateWithImpossibleQuartileOrderThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new NumericColumnStatistics(q1: 10, median: 5, q3: 12));
    }

    [Fact]
    public void CreateWithNegativeStandardDeviationThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new NumericColumnStatistics(standardDeviation: -0.1m));
    }
}
