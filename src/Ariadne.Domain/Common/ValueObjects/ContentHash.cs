using Ariadne.Domain.Common;

namespace Ariadne.Domain.Common.ValueObjects;

public sealed record ContentHash
{
    public const string Sha256Algorithm = "SHA256";

    private static readonly HashSet<string> _knownAlgorithms = new(StringComparer.OrdinalIgnoreCase)
    {
        Sha256Algorithm,
    };

    public ContentHash(string algorithm, string value)
    {
        string normalizedAlgorithm = string.IsNullOrWhiteSpace(algorithm)
            ? throw new DomainException("Content hash algorithm is required.")
            : algorithm.Trim().ToUpperInvariant();

        if (!_knownAlgorithms.Contains(normalizedAlgorithm))
            throw new DomainException($"Content hash algorithm '{normalizedAlgorithm}' is not supported.");

        Algorithm = normalizedAlgorithm;
        Value = string.IsNullOrWhiteSpace(value)
            ? throw new DomainException("Content hash value is required.")
            : value.Trim();
    }

    public string Algorithm { get; }

    public string Value { get; }

    public override string ToString() => $"{Algorithm}:{Value}";
}
