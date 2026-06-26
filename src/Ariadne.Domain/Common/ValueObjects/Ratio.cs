using System.Globalization;
using Ariadne.Domain.Common;

namespace Ariadne.Domain.Common.ValueObjects;

public readonly record struct Ratio
{
    public Ratio(double value)
    {
        if (double.IsNaN(value))
            throw new DomainException("Ratio must not be NaN.");

        if (double.IsInfinity(value))
            throw new DomainException("Ratio must be finite.");

        if (value is < 0 or > 1)
            throw new DomainException("Ratio must be between 0 and 1.");

        Value = value;
    }

    public double Value { get; }

    public static Ratio Zero => new(0);

    public static Ratio One => new(1);

    public override string ToString() => Value.ToString("G", CultureInfo.InvariantCulture);
}
