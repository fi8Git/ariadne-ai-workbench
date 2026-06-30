using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;

namespace Ariadne.Domain.Profiling;

public sealed record InferredValue<T> where T : struct, Enum
{
    public InferredValue(T value, Ratio confidence, string? reason, bool needsReview)
    {
        if (!Enum.IsDefined(value))
            throw new DomainException($"Inferred value '{value}' is not defined for {typeof(T).Name}.");

        Value = value;
        Confidence = confidence;
        Reason = NormalizeOptionalText(reason);
        NeedsReview = needsReview;
    }

    public T Value { get; }

    public Ratio Confidence { get; }

    public string? Reason { get; }

    public bool NeedsReview { get; }

    private static string? NormalizeOptionalText(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
