using Ariadne.Domain.Common;

namespace Ariadne.Domain.Profiling;

public sealed record DistinctValueSummary
{
    public DistinctValueSummary(long uniqueCount)
    {
        if (uniqueCount < 0)
            throw new DomainException("Unique value count must be non-negative.");

        UniqueCount = uniqueCount;
    }

    public long UniqueCount { get; }
}
