using Ariadne.Domain.Common;
using Ariadne.Domain.Decisions;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Tests.Decisions;

public class DecisionLogEntryTests
{
    private static readonly DateTimeOffset CreatedAt = new(2026, 6, 30, 11, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset NonUtc = new(2026, 6, 30, 11, 0, 0, TimeSpan.FromHours(2));

    [Fact]
    public void CreateWithValidDataInitializesOpenDecision()
    {
        ProjectId projectId = ProjectId.New();
        DecisionLogEntryId entryId = DecisionLogEntryId.New();

        DecisionLogEntry entry = DecisionLogEntry.Create(
            entryId,
            projectId,
            DecisionEntryType.Decision,
            "  Ignore customer_id  ",
            "  It is an identifier, not a meaningful feature.  ",
            CreatedAt,
            "  Variable review  ",
            DecisionImpact.Medium);

        Assert.Equal(entryId, entry.Id);
        Assert.Equal(projectId, entry.ProjectId);
        Assert.Equal(DecisionEntryType.Decision, entry.Type);
        Assert.Equal(DecisionStatus.Open, entry.Status);
        Assert.Equal("Ignore customer_id", entry.Title);
        Assert.Equal("It is an identifier, not a meaningful feature.", entry.Rationale);
        Assert.Equal("Variable review", entry.Evidence);
        Assert.Equal(DecisionImpact.Medium, entry.Impact);
        Assert.Equal(CreatedAt, entry.CreatedAtUtc);
        Assert.Equal(CreatedAt, entry.UpdatedAtUtc);
        Assert.Null(entry.ResolvedAtUtc);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateWithoutTitleThrowsDomainException(string? title)
    {
        Assert.Throws<DomainException>(
            () => DecisionLogEntry.Create(
                DecisionLogEntryId.New(),
                ProjectId.New(),
                DecisionEntryType.Decision,
                title!,
                "Clear rationale.",
                CreatedAt));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateWithoutRationaleThrowsDomainException(string? rationale)
    {
        Assert.Throws<DomainException>(
            () => DecisionLogEntry.Create(
                DecisionLogEntryId.New(),
                ProjectId.New(),
                DecisionEntryType.Decision,
                "Ignore customer_id",
                rationale!,
                CreatedAt));
    }

    [Fact]
    public void CreateWithDefaultProjectIdThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => DecisionLogEntry.Create(
                DecisionLogEntryId.New(),
                default,
                DecisionEntryType.Decision,
                "Ignore customer_id",
                "It is an identifier.",
                CreatedAt));
    }

    [Fact]
    public void CreateWithDefaultEntryIdThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => DecisionLogEntry.Create(
                default,
                ProjectId.New(),
                DecisionEntryType.Decision,
                "Ignore customer_id",
                "It is an identifier.",
                CreatedAt));
    }

    [Fact]
    public void CreateWithNonUtcTimestampThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => DecisionLogEntry.Create(
                DecisionLogEntryId.New(),
                ProjectId.New(),
                DecisionEntryType.Decision,
                "Ignore customer_id",
                "It is an identifier.",
                NonUtc));
    }

    [Fact]
    public void CreateWithUndefinedTypeThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => DecisionLogEntry.Create(
                DecisionLogEntryId.New(),
                ProjectId.New(),
                (DecisionEntryType)999,
                "Ignore customer_id",
                "It is an identifier.",
                CreatedAt));
    }

    [Fact]
    public void CreateWithUndefinedImpactThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => DecisionLogEntry.Create(
                DecisionLogEntryId.New(),
                ProjectId.New(),
                DecisionEntryType.Decision,
                "Ignore customer_id",
                "It is an identifier.",
                CreatedAt,
                impact: (DecisionImpact)999));
    }

    [Fact]
    public void UpdateDetailsTrimsValuesAndNormalizesBlankEvidence()
    {
        DecisionLogEntry entry = CreateEntry();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(1);

        entry.UpdateDetails(
            "  Keep surface column  ",
            "  It is central to the project objective despite missing values.  ",
            " ",
            updatedAt);

        Assert.Equal("Keep surface column", entry.Title);
        Assert.Equal("It is central to the project objective despite missing values.", entry.Rationale);
        Assert.Null(entry.Evidence);
        Assert.Equal(updatedAt, entry.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateDetailsWithoutRationaleThrowsDomainExceptionAndLeavesStateUnchanged()
    {
        DecisionLogEntry entry = CreateEntry();

        Assert.Throws<DomainException>(
            () => entry.UpdateDetails("New title", "", "New evidence", CreatedAt.AddMinutes(1)));

        Assert.Equal("Ignore customer_id", entry.Title);
        Assert.Equal("It is an identifier.", entry.Rationale);
        Assert.Equal(CreatedAt, entry.UpdatedAtUtc);
    }

    [Fact]
    public void ChangeStatusToResolvedSetsResolvedTimestamp()
    {
        DecisionLogEntry entry = CreateEntry();
        DateTimeOffset resolvedAt = CreatedAt.AddMinutes(1);

        entry.MarkResolved(resolvedAt);

        Assert.Equal(DecisionStatus.Resolved, entry.Status);
        Assert.Equal(resolvedAt, entry.ResolvedAtUtc);
        Assert.Equal(resolvedAt, entry.UpdatedAtUtc);
    }

    [Fact]
    public void ChangeStatusAwayFromResolvedClearsResolvedTimestamp()
    {
        DecisionLogEntry entry = CreateEntry();
        entry.MarkResolved(CreatedAt.AddMinutes(1));

        entry.ChangeStatus(DecisionStatus.Open, CreatedAt.AddMinutes(2));

        Assert.Equal(DecisionStatus.Open, entry.Status);
        Assert.Null(entry.ResolvedAtUtc);
    }

    [Fact]
    public void ChangeStatusRejectsUndefinedStatus()
    {
        DecisionLogEntry entry = CreateEntry();

        Assert.Throws<DomainException>(() => entry.ChangeStatus((DecisionStatus)999, CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void ChangeTypeAndImpactValidateEnumValues()
    {
        DecisionLogEntry entry = CreateEntry();

        entry.ChangeType(DecisionEntryType.Limitation, CreatedAt.AddMinutes(1));
        entry.ChangeImpact(DecisionImpact.High, CreatedAt.AddMinutes(2));

        Assert.Equal(DecisionEntryType.Limitation, entry.Type);
        Assert.Equal(DecisionImpact.High, entry.Impact);
        Assert.Throws<DomainException>(() => entry.ChangeType((DecisionEntryType)999, CreatedAt.AddMinutes(3)));
        Assert.Throws<DomainException>(() => entry.ChangeImpact((DecisionImpact)999, CreatedAt.AddMinutes(3)));
    }

    [Fact]
    public void UpdateBeforeLastUpdateThrowsDomainExceptionAndLeavesStateUnchanged()
    {
        DecisionLogEntry entry = CreateEntry();
        entry.ChangeStatus(DecisionStatus.Confirmed, CreatedAt.AddMinutes(2));

        Assert.Throws<DomainException>(
            () => entry.UpdateDetails("Older title", "Older rationale.", null, CreatedAt.AddMinutes(1)));

        Assert.Equal(DecisionStatus.Confirmed, entry.Status);
        Assert.Equal("Ignore customer_id", entry.Title);
        Assert.Equal(CreatedAt.AddMinutes(2), entry.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateWithNonUtcTimestampThrowsDomainExceptionAndLeavesStateUnchanged()
    {
        DecisionLogEntry entry = CreateEntry();

        Assert.Throws<DomainException>(
            () => entry.UpdateDetails("New title", "New rationale.", null, NonUtc.AddMinutes(1)));

        Assert.Equal("Ignore customer_id", entry.Title);
        Assert.Equal(CreatedAt, entry.UpdatedAtUtc);
    }

    private static DecisionLogEntry CreateEntry()
        => DecisionLogEntry.Create(
            DecisionLogEntryId.New(),
            ProjectId.New(),
            DecisionEntryType.Decision,
            "Ignore customer_id",
            "It is an identifier.",
            CreatedAt);
}
