using Ariadne.Domain.Common;
using Ariadne.Domain.Datasets;
using Ariadne.Domain.Methodology;

namespace Ariadne.Domain.Projects;

public sealed class AiProject : AggregateRoot<ProjectId>
{
    private readonly List<DatasetId> _datasetIds = [];

    private AiProject(
        ProjectId id,
        ProjectName name,
        DateTimeOffset createdAtUtc,
        string? description,
        string? objective)
        : base(DomainGuard.EnsureNotDefaultId(id, id.Value, "Project ID is required."))
    {
        createdAtUtc = DomainGuard.EnsureUtc(createdAtUtc, nameof(createdAtUtc));

        Name = name ?? throw new DomainException("Project name is required.");
        Description = NormalizeOptionalText(description);
        Objective = NormalizeOptionalText(objective);
        Status = ProjectStatus.Active;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = createdAtUtc;
        Progress = MethodologyProgress.CreateForMvp(createdAtUtc);
    }

    public ProjectName Name { get; private set; }

    public string? Description { get; private set; }

    public string? Objective { get; private set; }

    public ProjectStatus Status { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; }

    public DateTimeOffset UpdatedAtUtc { get; private set; }

    public DatasetVersionId? ActiveDatasetVersionId { get; private set; }

    public MethodologyProgress Progress { get; private set; }

    public IReadOnlyCollection<DatasetId> DatasetIds => _datasetIds.AsReadOnly();

    public static AiProject Create(
        ProjectId id,
        ProjectName name,
        DateTimeOffset now,
        string? description = null,
        string? objective = null)
        => new(id, name, now, description, objective);

    public void Rename(ProjectName name, DateTimeOffset now)
    {
        EnsureCanModify(now);
        Name = name ?? throw new DomainException("Project name is required.");
        Touch(now);
    }

    public void ChangeDescription(string? description, DateTimeOffset now)
    {
        EnsureCanModify(now);
        Description = NormalizeOptionalText(description);
        Touch(now);
    }

    public void DefineObjective(string? objective, DateTimeOffset now)
    {
        EnsureCanModify(now);
        Objective = NormalizeOptionalText(objective);
        Touch(now);
    }

    public void AttachDataset(DatasetId datasetId, DateTimeOffset now)
    {
        EnsureCanModify(now);
        DomainGuard.EnsureNotDefaultId(datasetId, datasetId.Value, "Dataset ID is required.");

        if (_datasetIds.Contains(datasetId))
            return;

        _datasetIds.Add(datasetId);
        Progress = Progress.WithStatus(MethodologyStep.Dataset, MethodologyStepStatus.InProgress, now);
        Touch(now);
    }

    public void SetActiveDatasetVersion(DatasetVersionId datasetVersionId, DateTimeOffset now)
    {
        EnsureCanModify(now);
        DomainGuard.EnsureNotDefaultId(datasetVersionId, datasetVersionId.Value, "Dataset version ID is required.");

        ActiveDatasetVersionId = datasetVersionId;
        Progress = Progress.WithStatus(MethodologyStep.Dataset, MethodologyStepStatus.InProgress, now);
        Touch(now);
    }

    public void UpdateStepStatus(MethodologyStep step, MethodologyStepStatus status, DateTimeOffset now)
    {
        EnsureCanModify(now);
        MethodologyProgress progress = Progress.WithStatus(step, status, now);
        Progress = progress;
        Touch(now);
    }

    public void Archive(DateTimeOffset now)
    {
        EnsureCanModify(now);
        Status = ProjectStatus.Archived;
        Touch(now);
    }

    public void Reactivate(DateTimeOffset now)
    {
        if (Status == ProjectStatus.Active)
            return;

        EnsureUpdateTimestamp(now);
        Status = ProjectStatus.Active;
        Touch(now);
    }

    private void Touch(DateTimeOffset now) => UpdatedAtUtc = now;

    private void EnsureCanModify(DateTimeOffset now)
    {
        EnsureActive();
        EnsureUpdateTimestamp(now);
    }

    private void EnsureUpdateTimestamp(DateTimeOffset now)
    {
        DomainGuard.EnsureUtc(now, nameof(now));

        if (now < UpdatedAtUtc)
            throw new DomainException("Updated timestamp cannot be before the last project update.");
    }

    private void EnsureActive()
    {
        if (Status == ProjectStatus.Archived)
            throw new DomainException("Archived projects cannot be modified.");
    }

    private static string? NormalizeOptionalText(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
