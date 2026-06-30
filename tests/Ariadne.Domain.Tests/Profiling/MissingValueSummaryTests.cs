using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class MissingValueSummaryTests
{
    [Fact]
    public void CreateStoresMissingValueMetadata()
    {
        MissingValueSummary summary = MissingValueSummary.Create(
            3,
            0.3,
            MissingValueSeverity.High);

        Assert.Equal(3, summary.MissingCount);
        Assert.Equal(0.3, summary.MissingRatio.Value);
        Assert.Equal(MissingValueSeverity.High, summary.Severity);
    }

    [Fact]
    public void CreateWithNegativeCountThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new MissingValueSummary(-1, Ratio.Zero, MissingValueSeverity.Low));
    }

    [Fact]
    public void CreateWithInvalidRatioThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => MissingValueSummary.Create(1, -0.1, MissingValueSeverity.Low));
    }

    [Fact]
    public void CreateWithUndefinedSeverityThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new MissingValueSummary(1, Ratio.One, (MissingValueSeverity)999));
    }
}
