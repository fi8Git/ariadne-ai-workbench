using Ariadne.Domain.Common;
using Ariadne.Domain.Profiling;
using Ariadne.Domain.Tests.Profiling;
using Ariadne.Domain.Variables;

namespace Ariadne.Domain.Tests.Variables;

public class VariableDefinitionTests
{
    private static readonly DateTimeOffset CreatedAt = new(2026, 6, 29, 15, 0, 0, TimeSpan.Zero);

    [Fact]
    public void FromColumnProfileStartsAsNeedsReviewWithInferredValues()
    {
        ColumnProfile profile = ColumnProfileTests.CreateNumericColumn(new ColumnName("price"));

        VariableDefinition variable = VariableDefinition.FromColumnProfile(profile, CreatedAt);

        Assert.Equal("price", variable.ColumnName.Value);
        Assert.Equal(PrimitiveDataType.Decimal, variable.InferredPrimitiveType);
        Assert.Equal(PrimitiveDataType.Decimal, variable.PrimitiveType);
        Assert.Equal(MethodologicalVariableType.Continuous, variable.InferredMethodologicalType);
        Assert.Equal(MethodologicalVariableType.Continuous, variable.MethodologicalType);
        Assert.Equal(VariableRole.Unknown, variable.Role);
        Assert.Equal(VariableReviewStatus.NeedsReview, variable.ReviewStatus);
        Assert.False(variable.HasMethodologicalTypeOverride);
        Assert.False(variable.IsReviewedOrIgnored);
        Assert.Equal(CreatedAt, variable.CreatedAtUtc);
        Assert.Equal(CreatedAt, variable.UpdatedAtUtc);
        Assert.Null(variable.ReviewedAtUtc);
    }

    [Fact]
    public void FromColumnProfileRequiresProfile()
    {
        Assert.Throws<DomainException>(() => VariableDefinition.FromColumnProfile(null!, CreatedAt));
    }

    [Fact]
    public void ChangeDocumentationFieldsTrimsTextAndUpdatesTimestamp()
    {
        VariableDefinition variable = CreateVariable();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(1);

        variable.ChangeDisplayName("  Sale price  ", updatedAt);
        variable.ChangeDescription("  Final sale price  ", updatedAt.AddMinutes(1));
        variable.ChangeUnit(new UnitOfMeasure(" EUR "), updatedAt.AddMinutes(2));
        variable.ChangeSourceNotes("  CRM export  ", updatedAt.AddMinutes(3));
        variable.ChangeQualityNotes("  Missing for cancelled orders  ", updatedAt.AddMinutes(4));
        variable.ChangeMissingValueInterpretation("  Unknown or not applicable  ", updatedAt.AddMinutes(5));

        Assert.Equal("Sale price", variable.DisplayName);
        Assert.Equal("Final sale price", variable.Description);
        Assert.Equal("EUR", variable.Unit?.Value);
        Assert.Equal("CRM export", variable.SourceNotes);
        Assert.Equal("Missing for cancelled orders", variable.QualityNotes);
        Assert.Equal("Unknown or not applicable", variable.MissingValueInterpretation);
        Assert.Equal(updatedAt.AddMinutes(5), variable.UpdatedAtUtc);
    }

    [Fact]
    public void ChangeDocumentationFieldsNormalizeBlankTextToNull()
    {
        VariableDefinition variable = CreateVariable();

        variable.ChangeDisplayName(" ", CreatedAt.AddMinutes(1));
        variable.ChangeDescription("", CreatedAt.AddMinutes(2));
        variable.ChangeUnit(null, CreatedAt.AddMinutes(3));
        variable.ChangeSourceNotes(null, CreatedAt.AddMinutes(4));
        variable.ChangeQualityNotes(" ", CreatedAt.AddMinutes(5));
        variable.ChangeMissingValueInterpretation("", CreatedAt.AddMinutes(6));

        Assert.Null(variable.DisplayName);
        Assert.Null(variable.Description);
        Assert.Null(variable.Unit);
        Assert.Null(variable.SourceNotes);
        Assert.Null(variable.QualityNotes);
        Assert.Null(variable.MissingValueInterpretation);
    }

    [Fact]
    public void ChangeMethodologicalTypeRequiresReason()
    {
        VariableDefinition variable = CreateVariable();

        Assert.Throws<DomainException>(
            () => variable.ChangeMethodologicalType(
                MethodologicalVariableType.Discrete,
                " ",
                CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void ChangeMethodologicalTypeStoresReviewedValueSeparatelyFromInferredValueAndResetsReview()
    {
        VariableDefinition variable = CreateVariable();
        variable.MarkReviewed(CreatedAt.AddMinutes(1));

        variable.ChangeMethodologicalType(
            MethodologicalVariableType.Discrete,
            "Integer-like count should be discrete.",
            CreatedAt.AddMinutes(2));

        Assert.Equal(MethodologicalVariableType.Continuous, variable.InferredMethodologicalType);
        Assert.Equal(MethodologicalVariableType.Discrete, variable.MethodologicalType);
        Assert.True(variable.HasMethodologicalTypeOverride);
        Assert.Equal(VariableReviewStatus.NeedsReview, variable.ReviewStatus);
        Assert.Null(variable.ReviewedAtUtc);
    }

    [Fact]
    public void ChangeMethodologicalTypeRejectsUndefinedType()
    {
        VariableDefinition variable = CreateVariable();

        Assert.Throws<DomainException>(
            () => variable.ChangeMethodologicalType(
                (MethodologicalVariableType)999,
                "Invalid.",
                CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void ChangeRoleRequiresReason()
    {
        VariableDefinition variable = CreateVariable();

        Assert.Throws<DomainException>(
            () => variable.ChangeRole(VariableRole.Feature, "", CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void ChangeRoleToFeatureResetsReviewedStatus()
    {
        VariableDefinition variable = CreateVariable();
        variable.MarkReviewed(CreatedAt.AddMinutes(1));

        variable.ChangeRole(VariableRole.Feature, "Confirmed as input feature.", CreatedAt.AddMinutes(2));

        Assert.Equal(VariableRole.Feature, variable.Role);
        Assert.Equal(VariableReviewStatus.NeedsReview, variable.ReviewStatus);
        Assert.Null(variable.ReviewedAtUtc);
    }

    [Fact]
    public void ChangeRoleToIgnoredSetsIgnoredStatus()
    {
        VariableDefinition variable = CreateVariable();

        variable.ChangeRole(VariableRole.Ignored, "Identifier should not be analysed.", CreatedAt.AddMinutes(1));

        Assert.Equal(VariableRole.Ignored, variable.Role);
        Assert.Equal(VariableReviewStatus.Ignored, variable.ReviewStatus);
        Assert.Null(variable.ReviewedAtUtc);
        Assert.True(variable.IsReviewedOrIgnored);
    }

    [Fact]
    public void ChangeRoleRejectsUndefinedRole()
    {
        VariableDefinition variable = CreateVariable();

        Assert.Throws<DomainException>(
            () => variable.ChangeRole((VariableRole)999, "Invalid.", CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void MarkReviewedSetsReviewStatusAndTimestamp()
    {
        VariableDefinition variable = CreateVariable();
        DateTimeOffset reviewedAt = CreatedAt.AddMinutes(1);

        variable.MarkReviewed(reviewedAt);

        Assert.Equal(VariableReviewStatus.Reviewed, variable.ReviewStatus);
        Assert.Equal(reviewedAt, variable.ReviewedAtUtc);
        Assert.Equal(reviewedAt, variable.UpdatedAtUtc);
        Assert.True(variable.IsReviewedOrIgnored);
    }

    [Fact]
    public void MarkIgnoredRequiresReason()
    {
        VariableDefinition variable = CreateVariable();

        Assert.Throws<DomainException>(() => variable.MarkIgnored(" ", CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void MarkIgnoredSetsRoleAndStatus()
    {
        VariableDefinition variable = CreateVariable();

        variable.MarkIgnored("Not useful for methodology.", CreatedAt.AddMinutes(1));

        Assert.Equal(VariableRole.Ignored, variable.Role);
        Assert.Equal(VariableReviewStatus.Ignored, variable.ReviewStatus);
        Assert.Null(variable.ReviewedAtUtc);
        Assert.True(variable.IsReviewedOrIgnored);
    }

    [Fact]
    public void UpdateBeforeLastUpdateThrowsDomainExceptionAndLeavesStateUnchanged()
    {
        VariableDefinition variable = CreateVariable();
        variable.ChangeDescription("Reviewed meaning.", CreatedAt.AddMinutes(2));

        Assert.Throws<DomainException>(
            () => variable.ChangeDisplayName("Older update", CreatedAt.AddMinutes(1)));

        Assert.Null(variable.DisplayName);
        Assert.Equal("Reviewed meaning.", variable.Description);
        Assert.Equal(CreatedAt.AddMinutes(2), variable.UpdatedAtUtc);
    }

    private static VariableDefinition CreateVariable()
        => VariableDefinition.FromColumnProfile(
            ColumnProfileTests.CreateNumericColumn(new ColumnName("price")),
            CreatedAt);
}
