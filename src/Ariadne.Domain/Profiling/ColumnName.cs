using Ariadne.Domain.Common;

namespace Ariadne.Domain.Profiling;

public sealed record ColumnName
{
    public ColumnName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Column name is required.");

        Value = value.Trim();
    }

    public string Value { get; }

    public override string ToString() => Value;
}
