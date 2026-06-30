using Ariadne.Domain.Common;

namespace Ariadne.Domain.Datasets;

public sealed record DatasetName
{
    public const int MaxLength = 200;

    public DatasetName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Dataset name is required.");

        Value = value.Trim();

        if (Value.Length > MaxLength)
            throw new DomainException("Dataset name must be 200 characters or fewer.");
    }

    public string Value { get; }

    public override string ToString() => Value;
}
