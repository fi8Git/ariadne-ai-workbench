using Ariadne.Domain.Common;

namespace Ariadne.Domain.Profiling;

public sealed record NumericColumnStatistics
{
    public NumericColumnStatistics(
        decimal? minimum = null,
        decimal? maximum = null,
        decimal? mean = null,
        decimal? median = null,
        decimal? q1 = null,
        decimal? q3 = null,
        decimal? standardDeviation = null)
    {
        if (minimum > maximum)
            throw new DomainException("Minimum must be less than or equal to maximum.");

        if (q1 > median)
            throw new DomainException("Q1 must be less than or equal to median.");

        if (median > q3)
            throw new DomainException("Median must be less than or equal to Q3.");

        if (q1 > q3)
            throw new DomainException("Q1 must be less than or equal to Q3.");

        if (standardDeviation < 0)
            throw new DomainException("Standard deviation must be non-negative.");

        Minimum = minimum;
        Maximum = maximum;
        Mean = mean;
        Median = median;
        Q1 = q1;
        Q3 = q3;
        StandardDeviation = standardDeviation;
    }

    public decimal? Minimum { get; }

    public decimal? Maximum { get; }

    public decimal? Mean { get; }

    public decimal? Median { get; }

    public decimal? Q1 { get; }

    public decimal? Q3 { get; }

    public decimal? StandardDeviation { get; }
}
