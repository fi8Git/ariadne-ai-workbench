using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;

namespace Ariadne.Domain.Profiling;

public sealed record MissingValueSummary
{
    public MissingValueSummary(long missingCount, Ratio missingRatio, MissingValueSeverity severity)
    {
        if (missingCount < 0)
            throw new DomainException("Missing value count must be non-negative.");

        if (!Enum.IsDefined(severity))
            throw new DomainException("Missing value severity is not defined.");

        MissingCount = missingCount;
        MissingRatio = missingRatio;
        Severity = severity;
    }

    public long MissingCount { get; }

    public Ratio MissingRatio { get; }

    public MissingValueSeverity Severity { get; }

    public static MissingValueSummary Create(
        long missingCount,
        double missingRatio,
        MissingValueSeverity severity)
        => new(missingCount, new Ratio(missingRatio), severity);
}
