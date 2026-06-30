using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;

namespace Ariadne.Domain.Profiling;

public sealed record ValueCount
{
    public ValueCount(string displayValue, long count, Ratio ratio)
    {
        if (displayValue is null)
            throw new DomainException("Value count display value is required.");

        if (count < 0)
            throw new DomainException("Value count must be non-negative.");

        DisplayValue = displayValue;
        Count = count;
        Ratio = ratio;
    }

    public string DisplayValue { get; }

    public long Count { get; }

    public Ratio Ratio { get; }

    public static ValueCount Create(string displayValue, long count, double ratio)
        => new(displayValue, count, new Ratio(ratio));
}
