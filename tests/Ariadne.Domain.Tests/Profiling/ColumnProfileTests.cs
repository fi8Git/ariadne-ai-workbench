using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class ColumnProfileTests
{
    [Fact]
    public void CreateTextColumnWithoutNumericStatisticsSucceeds()
    {
        ColumnProfile profile = CreateTextColumn();

        Assert.Equal("city", profile.ColumnName.Value);
        Assert.Equal(PrimitiveDataType.Text, profile.InferredPrimitiveType.Value);
        Assert.Equal(MethodologicalVariableType.Discrete, profile.InferredMethodologicalType.Value);
        Assert.Null(profile.NumericStatistics);
        Assert.Null(profile.Outliers);
        Assert.Equal(["Paris", "Lyon"], profile.SampleValues);
        Assert.Equal(["ambiguous categories"], profile.Warnings);
    }

    [Fact]
    public void CreateNumericColumnWithStatisticsSucceeds()
    {
        ColumnProfile profile = CreateNumericColumn();

        Assert.Equal(PrimitiveDataType.Decimal, profile.InferredPrimitiveType.Value);
        Assert.NotNull(profile.NumericStatistics);
        Assert.NotNull(profile.Outliers);
    }

    [Fact]
    public void CreateNonNumericColumnWithNumericStatisticsThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => CreateTextColumn(numericStatistics: new NumericColumnStatistics(minimum: 1, maximum: 2)));
    }

    [Fact]
    public void CreateNonNumericColumnWithOutliersThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => CreateTextColumn(outliers: new OutlierCandidateSummary("IQR", 1)));
    }

    [Fact]
    public void CreateWithMissingRequiredPartsThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new ColumnProfile(
                null!,
                new InferredValue<PrimitiveDataType>(PrimitiveDataType.Text, Ratio.One, null, false),
                new InferredValue<MethodologicalVariableType>(MethodologicalVariableType.Text, Ratio.One, null, false),
                new MissingValueSummary(0, Ratio.Zero, MissingValueSeverity.None),
                new DistinctValueSummary(1)));
    }

    [Fact]
    public void CreateWithNullSampleValueThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => CreateTextColumn(sampleValues: ["Paris", null!]));
    }

    internal static ColumnProfile CreateTextColumn(
        ColumnName? columnName = null,
        NumericColumnStatistics? numericStatistics = null,
        OutlierCandidateSummary? outliers = null,
        IEnumerable<string>? sampleValues = null)
        => new(
            columnName ?? new ColumnName("city"),
            new InferredValue<PrimitiveDataType>(PrimitiveDataType.Text, new Ratio(0.95), "mostly strings", false),
            new InferredValue<MethodologicalVariableType>(MethodologicalVariableType.Discrete, new Ratio(0.7), "few repeated values", true),
            MissingValueSummary.Create(1, 0.1, MissingValueSeverity.Low),
            new DistinctValueSummary(2),
            numericStatistics,
            outliers,
            [ValueCount.Create("Paris", 6, 0.6)],
            sampleValues ?? ["Paris", "Lyon"],
            ["ambiguous categories"]);

    internal static ColumnProfile CreateNumericColumn(ColumnName? columnName = null)
        => new(
            columnName ?? new ColumnName("price"),
            new InferredValue<PrimitiveDataType>(PrimitiveDataType.Decimal, new Ratio(0.98), "numeric sample", false),
            new InferredValue<MethodologicalVariableType>(MethodologicalVariableType.Continuous, new Ratio(0.8), "many unique values", true),
            MissingValueSummary.Create(0, 0, MissingValueSeverity.None),
            new DistinctValueSummary(10),
            new NumericColumnStatistics(minimum: 1, maximum: 100, mean: 50, median: 51, q1: 25, q3: 75),
            new OutlierCandidateSummary("IQR", 1, lowerBound: -10, upperBound: 110),
            [],
            ["1", "2", "3"],
            []);
}
