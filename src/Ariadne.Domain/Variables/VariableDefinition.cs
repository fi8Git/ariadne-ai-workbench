using Ariadne.Domain.Common;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Variables;

public sealed class VariableDefinition : Entity<ColumnName>
{
    private VariableDefinition(
        ColumnName columnName,
        PrimitiveDataType inferredPrimitiveType,
        MethodologicalVariableType inferredMethodologicalType,
        DateTimeOffset now)
        : base(columnName ?? throw new DomainException("Column name is required."))
    {
        EnsureDefined(inferredPrimitiveType, "Primitive type is not defined.");
        EnsureDefined(inferredMethodologicalType, "Methodological type is not defined.");

        InferredPrimitiveType = inferredPrimitiveType;
        PrimitiveType = inferredPrimitiveType;
        InferredMethodologicalType = inferredMethodologicalType;
        MethodologicalType = inferredMethodologicalType;
        Role = VariableRole.Unknown;
        ReviewStatus = VariableReviewStatus.NeedsReview;
        CreatedAtUtc = now;
        UpdatedAtUtc = now;
    }

    public ColumnName ColumnName => Id;

    public string? DisplayName { get; private set; }

    public string? Description { get; private set; }

    public UnitOfMeasure? Unit { get; private set; }

    public PrimitiveDataType InferredPrimitiveType { get; }

    public PrimitiveDataType PrimitiveType { get; private set; }

    public MethodologicalVariableType InferredMethodologicalType { get; }

    public MethodologicalVariableType MethodologicalType { get; private set; }

    public VariableRole Role { get; private set; }

    public string? SourceNotes { get; private set; }

    public string? QualityNotes { get; private set; }

    public string? MissingValueInterpretation { get; private set; }

    public VariableReviewStatus ReviewStatus { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; }

    public DateTimeOffset UpdatedAtUtc { get; private set; }

    public DateTimeOffset? ReviewedAtUtc { get; private set; }

    public bool HasMethodologicalTypeOverride
        => MethodologicalType != InferredMethodologicalType;

    public bool IsReviewedOrIgnored
        => ReviewStatus is VariableReviewStatus.Reviewed or VariableReviewStatus.Ignored;

    public static VariableDefinition FromColumnProfile(ColumnProfile profile, DateTimeOffset now)
    {
        if (profile is null)
            throw new DomainException("Column profile is required.");

        return new VariableDefinition(
            profile.ColumnName,
            profile.InferredPrimitiveType.Value,
            profile.InferredMethodologicalType.Value,
            now);
    }

    public void ChangeDisplayName(string? displayName, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        DisplayName = NormalizeOptionalText(displayName);
        Touch(now);
    }

    public void ChangeDescription(string? description, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        Description = NormalizeOptionalText(description);
        Touch(now);
    }

    public void ChangeUnit(UnitOfMeasure? unit, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        Unit = unit;
        Touch(now);
    }

    public void ChangeMethodologicalType(MethodologicalVariableType type, string reason, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        EnsureReason(reason, "Changing a variable methodological type requires a reason.");
        EnsureDefined(type, "Methodological type is not defined.");

        MethodologicalType = type;
        MarkNeedsReview();
        Touch(now);
    }

    public void ChangeRole(VariableRole role, string reason, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        EnsureReason(reason, "Changing a variable role requires a reason.");
        EnsureDefined(role, "Variable role is not defined.");

        Role = role;

        if (role == VariableRole.Ignored)
        {
            ReviewStatus = VariableReviewStatus.Ignored;
            ReviewedAtUtc = null;
        }
        else
        {
            MarkNeedsReview();
        }

        Touch(now);
    }

    public void ChangeSourceNotes(string? notes, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        SourceNotes = NormalizeOptionalText(notes);
        Touch(now);
    }

    public void ChangeQualityNotes(string? notes, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        QualityNotes = NormalizeOptionalText(notes);
        Touch(now);
    }

    public void ChangeMissingValueInterpretation(string? interpretation, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        MissingValueInterpretation = NormalizeOptionalText(interpretation);
        Touch(now);
    }

    public void MarkReviewed(DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        ReviewStatus = VariableReviewStatus.Reviewed;
        ReviewedAtUtc = now;
        Touch(now);
    }

    public void MarkIgnored(string reason, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        EnsureReason(reason, "Ignoring a variable requires a reason.");

        Role = VariableRole.Ignored;
        ReviewStatus = VariableReviewStatus.Ignored;
        ReviewedAtUtc = null;
        Touch(now);
    }

    private void MarkNeedsReview()
    {
        ReviewStatus = VariableReviewStatus.NeedsReview;
        ReviewedAtUtc = null;
    }

    private void Touch(DateTimeOffset now) => UpdatedAtUtc = now;

    private void EnsureUpdateTimestamp(DateTimeOffset now)
    {
        if (now < UpdatedAtUtc)
            throw new DomainException("Updated timestamp cannot be before the last variable update.");
    }

    private static string? NormalizeOptionalText(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static void EnsureReason(string reason, string message)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException(message);
    }

    private static void EnsureDefined<TEnum>(TEnum value, string message) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(value))
            throw new DomainException(message);
    }
}
