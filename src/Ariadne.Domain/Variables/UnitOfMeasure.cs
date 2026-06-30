using Ariadne.Domain.Common;

namespace Ariadne.Domain.Variables;

public sealed record UnitOfMeasure
{
    public const int MaxLength = 100;

    public UnitOfMeasure(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Unit of measure is required.");

        Value = value.Trim();

        if (Value.Length > MaxLength)
            throw new DomainException("Unit of measure must be 100 characters or fewer.");
    }

    public string Value { get; }

    public override string ToString() => Value;
}
