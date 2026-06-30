using Ariadne.Domain.Common;
using Ariadne.Domain.Datasets;
using Ariadne.Domain.Methodology;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Tests.Projects;

public class AiProjectTests
{
    private static readonly DateTimeOffset CreatedAt = new(2026, 6, 26, 12, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset NonUtc = new(2026, 6, 26, 12, 0, 0, TimeSpan.FromHours(2));

    [Fact]
    public void CreateWithValidDataInitializesProject()
    {
        ProjectId projectId = ProjectId.New();

        AiProject project = AiProject.Create(
            projectId,
            new ProjectName("  Housing Study  "),
            CreatedAt,
            "  Predict sale prices  ",
            "  Understand pricing drivers  ");

        Assert.Equal(projectId, project.Id);
        Assert.Equal("Housing Study", project.Name.Value);
        Assert.Equal("Predict sale prices", project.Description);
        Assert.Equal("Understand pricing drivers", project.Objective);
        Assert.Equal(ProjectStatus.Active, project.Status);
        Assert.Equal(CreatedAt, project.CreatedAtUtc);
        Assert.Equal(CreatedAt, project.UpdatedAtUtc);
        Assert.Empty(project.DatasetIds);
        Assert.Null(project.ActiveDatasetVersionId);
        Assert.Equal(MethodologyStepStatus.Completed, project.Progress[MethodologyStep.Project].Status);
        Assert.Equal(MethodologyStepStatus.NotStarted, project.Progress[MethodologyStep.Dataset].Status);
    }

    [Fact]
    public void CreateWithInvalidNameThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => AiProject.Create(ProjectId.New(), new ProjectName(" "), CreatedAt));
    }

    [Fact]
    public void CreateWithDefaultProjectIdThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => AiProject.Create(default, new ProjectName("Housing Study"), CreatedAt));
    }

    [Fact]
    public void CreateWithNonUtcTimestampThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => AiProject.Create(ProjectId.New(), new ProjectName("Housing Study"), NonUtc));
    }

    [Fact]
    public void CreateNormalizesBlankOptionalMetadataToNull()
    {
        AiProject project = AiProject.Create(
            ProjectId.New(),
            new ProjectName("Housing Study"),
            CreatedAt,
            " ",
            "");

        Assert.Null(project.Description);
        Assert.Null(project.Objective);
    }

    [Fact]
    public void RenameUpdatesNameAndTimestamp()
    {
        AiProject project = CreateProject();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(10);

        project.Rename(new ProjectName("Rental Study"), updatedAt);

        Assert.Equal("Rental Study", project.Name.Value);
        Assert.Equal(updatedAt, project.UpdatedAtUtc);
    }

    [Fact]
    public void ChangeDescriptionTrimsDescriptionAndUpdatesTimestamp()
    {
        AiProject project = CreateProject();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(10);

        project.ChangeDescription("  Updated context  ", updatedAt);

        Assert.Equal("Updated context", project.Description);
        Assert.Equal(updatedAt, project.UpdatedAtUtc);
    }

    [Fact]
    public void DefineObjectiveTrimsObjectiveAndUpdatesTimestamp()
    {
        AiProject project = CreateProject();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(10);

        project.DefineObjective("  Explain price variance  ", updatedAt);

        Assert.Equal("Explain price variance", project.Objective);
        Assert.Equal(updatedAt, project.UpdatedAtUtc);
    }

    [Fact]
    public void AttachDatasetAddsDatasetOnceAndAdvancesDatasetStep()
    {
        AiProject project = CreateProject();
        DatasetId datasetId = DatasetId.New();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(10);

        project.AttachDataset(datasetId, updatedAt);
        project.AttachDataset(datasetId, updatedAt.AddMinutes(1));

        Assert.Equal([datasetId], project.DatasetIds);
        Assert.Equal(updatedAt, project.UpdatedAtUtc);
        Assert.Equal(MethodologyStepStatus.InProgress, project.Progress[MethodologyStep.Dataset].Status);
    }

    [Fact]
    public void AttachDatasetWithDefaultDatasetIdThrowsDomainException()
    {
        AiProject project = CreateProject();

        Assert.Throws<DomainException>(() => project.AttachDataset(default, CreatedAt.AddMinutes(10)));
    }

    [Fact]
    public void SetActiveDatasetVersionStoresVersionAndAdvancesDatasetStep()
    {
        AiProject project = CreateProject();
        DatasetVersionId versionId = DatasetVersionId.New();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(10);

        project.SetActiveDatasetVersion(versionId, updatedAt);

        Assert.Equal(versionId, project.ActiveDatasetVersionId);
        Assert.Equal(updatedAt, project.UpdatedAtUtc);
        Assert.Equal(MethodologyStepStatus.InProgress, project.Progress[MethodologyStep.Dataset].Status);
    }

    [Fact]
    public void SetActiveDatasetVersionWithDefaultVersionIdThrowsDomainException()
    {
        AiProject project = CreateProject();

        Assert.Throws<DomainException>(() => project.SetActiveDatasetVersion(default, CreatedAt.AddMinutes(10)));
    }

    [Fact]
    public void UpdateStepStatusUpdatesProgressAndTimestamp()
    {
        AiProject project = CreateProject();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(10);

        project.UpdateStepStatus(MethodologyStep.Report, MethodologyStepStatus.Completed, updatedAt);

        Assert.Equal(MethodologyStepStatus.Completed, project.Progress[MethodologyStep.Report].Status);
        Assert.Equal(updatedAt, project.Progress[MethodologyStep.Report].CompletedAtUtc);
        Assert.Equal(updatedAt, project.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateBeforeCreationThrowsDomainException()
    {
        AiProject project = CreateProject();

        Assert.Throws<DomainException>(
            () => project.Rename(new ProjectName("Too Early"), CreatedAt.AddTicks(-1)));

        Assert.Equal("Housing Study", project.Name.Value);
        Assert.Equal(CreatedAt, project.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateRejectsTimestampBeforeCurrentUpdatedAt()
    {
        AiProject project = CreateProject();
        project.ChangeDescription("First update.", CreatedAt.AddMinutes(10));

        Assert.Throws<DomainException>(
            () => project.Rename(new ProjectName("Older update"), CreatedAt.AddMinutes(5)));

        Assert.Equal("Housing Study", project.Name.Value);
        Assert.Equal(CreatedAt.AddMinutes(10), project.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateWithNonUtcTimestampThrowsDomainException()
    {
        AiProject project = CreateProject();

        Assert.Throws<DomainException>(
            () => project.Rename(new ProjectName("Non UTC"), NonUtc.AddMinutes(10)));

        Assert.Equal("Housing Study", project.Name.Value);
    }

    [Fact]
    public void ArchivePreventsFurtherModification()
    {
        AiProject project = CreateProject();

        project.Archive(CreatedAt.AddMinutes(10));

        Assert.Equal(ProjectStatus.Archived, project.Status);
        Assert.Throws<DomainException>(
            () => project.Rename(new ProjectName("Archived Rename"), CreatedAt.AddMinutes(11)));
    }

    [Fact]
    public void ReactivateAllowsSafeUpdatesAfterArchive()
    {
        AiProject project = CreateProject();

        project.Archive(CreatedAt.AddMinutes(10));
        project.Reactivate(CreatedAt.AddMinutes(11));
        project.Rename(new ProjectName("Reactivated"), CreatedAt.AddMinutes(12));

        Assert.Equal(ProjectStatus.Active, project.Status);
        Assert.Equal("Reactivated", project.Name.Value);
    }

    private static AiProject CreateProject()
        => AiProject.Create(ProjectId.New(), new ProjectName("Housing Study"), CreatedAt);
}
