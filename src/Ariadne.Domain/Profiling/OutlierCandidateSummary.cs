using Ariadne.Domain.Common;

namespace Ariadne.Domain.Profiling;

public sealed record OutlierCandidateSummary
{
    public OutlierCandidateSummary(
        string method,
        long candidateCount,
        decimal? lowerBound = null,
        decimal? upperBound = null,
        bool requiresContextReview = true)
    {
        if (candidateCount < 0)
            throw new DomainException("Outlier candidate count must be non-negative.");

        if (lowerBound > upperBound)
            throw new DomainException("Outlier lower bound must be less than or equal to upper bound.");

        if (string.IsNullOrWhiteSpace(method))
            throw new DomainException("Outlier detection method is required.");

        Method = method.Trim();
        CandidateCount = candidateCount;
        LowerBound = lowerBound;
        UpperBound = upperBound;
        RequiresContextReview = requiresContextReview;
    }

    public string Method { get; }

    public long CandidateCount { get; }

    public decimal? LowerBound { get; }

    public decimal? UpperBound { get; }

    public bool RequiresContextReview { get; }
}
