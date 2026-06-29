using Ariadne.Domain.Common;

namespace Ariadne.Domain.Methodology;

public sealed record MethodologyStepProgress
{
    public MethodologyStepProgress(
        MethodologyStep step,
        MethodologyStepStatus status,
        DateTimeOffset updatedAtUtc,
        DateTimeOffset? completedAtUtc = null,
        string? notes = null)
    {
        if (status == MethodologyStepStatus.Completed && completedAtUtc is null)
            throw new DomainException("Completed methodology steps require a completion timestamp.");

        if (status != MethodologyStepStatus.Completed && completedAtUtc is not null)
            throw new DomainException("Only completed methodology steps can have a completion timestamp.");

        Step = step;
        Status = status;
        UpdatedAtUtc = updatedAtUtc;
        CompletedAtUtc = completedAtUtc;
        Notes = NormalizeOptionalText(notes);
    }

    public MethodologyStep Step { get; }

    public MethodologyStepStatus Status { get; }

    public DateTimeOffset UpdatedAtUtc { get; }

    public DateTimeOffset? CompletedAtUtc { get; }

    public string? Notes { get; }

    public MethodologyStepProgress WithStatus(MethodologyStepStatus status, DateTimeOffset now, string? notes = null)
        => new(
            Step,
            status,
            now,
            status == MethodologyStepStatus.Completed ? now : null,
            notes);

    private static string? NormalizeOptionalText(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
