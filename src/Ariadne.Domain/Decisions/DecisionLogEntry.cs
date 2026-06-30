using Ariadne.Domain.Common;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Decisions;

public sealed class DecisionLogEntry : AggregateRoot<DecisionLogEntryId>
{
    private DecisionLogEntry(
        DecisionLogEntryId id,
        ProjectId projectId,
        DecisionEntryType type,
        string title,
        string rationale,
        DateTimeOffset createdAtUtc,
        string? evidence,
        DecisionImpact impact)
        : base(EnsureDecisionLogEntryId(id))
    {
        EnsureProjectId(projectId);
        EnsureDefined(type, "Decision entry type is not defined.");
        EnsureDefined(impact, "Decision impact is not defined.");

        ProjectId = projectId;
        Type = type;
        Status = DecisionStatus.Open;
        Title = NormalizeRequiredText(title, "Decision title is required.");
        Rationale = NormalizeRequiredText(rationale, "Decision rationale is required.");
        Evidence = NormalizeOptionalText(evidence);
        Impact = impact;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = createdAtUtc;
    }

    public ProjectId ProjectId { get; }

    public DecisionEntryType Type { get; private set; }

    public DecisionStatus Status { get; private set; }

    public string Title { get; private set; }

    public string Rationale { get; private set; }

    public string? Evidence { get; private set; }

    public DecisionImpact Impact { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; }

    public DateTimeOffset UpdatedAtUtc { get; private set; }

    public DateTimeOffset? ResolvedAtUtc { get; private set; }

    public static DecisionLogEntry Create(
        DecisionLogEntryId id,
        ProjectId projectId,
        DecisionEntryType type,
        string title,
        string rationale,
        DateTimeOffset now,
        string? evidence = null,
        DecisionImpact impact = DecisionImpact.Unknown)
        => new(id, projectId, type, title, rationale, now, evidence, impact);

    public void UpdateDetails(string title, string rationale, string? evidence, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        string normalizedTitle = NormalizeRequiredText(title, "Decision title is required.");
        string normalizedRationale = NormalizeRequiredText(rationale, "Decision rationale is required.");

        Title = normalizedTitle;
        Rationale = normalizedRationale;
        Evidence = NormalizeOptionalText(evidence);
        Touch(now);
    }

    public void ChangeType(DecisionEntryType type, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        EnsureDefined(type, "Decision entry type is not defined.");

        Type = type;
        Touch(now);
    }

    public void ChangeImpact(DecisionImpact impact, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        EnsureDefined(impact, "Decision impact is not defined.");

        Impact = impact;
        Touch(now);
    }

    public void ChangeStatus(DecisionStatus status, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        EnsureDefined(status, "Decision status is not defined.");

        Status = status;
        ResolvedAtUtc = status == DecisionStatus.Resolved ? now : null;
        Touch(now);
    }

    public void MarkResolved(DateTimeOffset now)
        => ChangeStatus(DecisionStatus.Resolved, now);

    private void Touch(DateTimeOffset now)
        => UpdatedAtUtc = now;

    private void EnsureUpdateTimestamp(DateTimeOffset now)
    {
        if (now < UpdatedAtUtc)
            throw new DomainException("Updated timestamp cannot be before the last decision log update.");
    }

    private static DecisionLogEntryId EnsureDecisionLogEntryId(DecisionLogEntryId id)
    {
        if (id.Value == Guid.Empty)
            throw new DomainException("Decision log entry ID is required.");

        return id;
    }

    private static void EnsureProjectId(ProjectId projectId)
    {
        if (projectId.Value == Guid.Empty)
            throw new DomainException("Project ID is required.");
    }

    private static string NormalizeRequiredText(string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(message);

        return value.Trim();
    }

    private static string? NormalizeOptionalText(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static void EnsureDefined<TEnum>(TEnum value, string message) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(value))
            throw new DomainException(message);
    }
}
