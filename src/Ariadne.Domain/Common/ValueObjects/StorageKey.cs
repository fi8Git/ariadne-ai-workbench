using Ariadne.Domain.Common;

namespace Ariadne.Domain.Common.ValueObjects;

public sealed record StorageKey
{
    public StorageKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Storage key is required.");

        Value = value.Trim();

        if (Value.StartsWith('/') || Value.StartsWith('\\') || Value.Contains('\\') || Value.Contains(':'))
            throw new DomainException("Storage key must be a relative logical path.");

        string[] segments = Value.Split('/');

        if (segments.Any(segment => segment is "" or "." or ".."))
            throw new DomainException("Storage key contains an invalid path segment.");
    }

    public string Value { get; }

    public override string ToString() => Value;
}
