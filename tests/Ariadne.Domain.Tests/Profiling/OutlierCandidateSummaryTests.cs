using Ariadne.Domain.Common;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class OutlierCandidateSummaryTests
{
    [Fact]
    public void CreateTrimsMethodAndRequiresContextReviewByDefault()
    {
        var summary = new OutlierCandidateSummary(" IQR ", 2, lowerBound: 1, upperBound: 10);

        Assert.Equal("IQR", summary.Method);
        Assert.Equal(2, summary.CandidateCount);
        Assert.Equal(1, summary.LowerBound);
        Assert.Equal(10, summary.UpperBound);
        Assert.True(summary.RequiresContextReview);
    }

    [Fact]
    public void CreateWithNegativeCandidateCountThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new OutlierCandidateSummary("IQR", -1));
    }

    [Fact]
    public void CreateWithMissingMethodThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new OutlierCandidateSummary(" ", 1));
    }

    [Fact]
    public void CreateWithLowerBoundGreaterThanUpperBoundThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new OutlierCandidateSummary("IQR", 1, lowerBound: 10, upperBound: 1));
    }
}
