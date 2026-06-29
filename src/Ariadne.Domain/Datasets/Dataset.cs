using Ariadne.Domain.Common;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Datasets;

public sealed class Dataset : AggregateRoot<DatasetId>
{
    private readonly List<DatasetVersion> _versions = [];

    private Dataset(
        DatasetId id,
        ProjectId projectId,
        DatasetName name,
        DataSourceKind sourceKind,
        DateTimeOffset now,
        string? description)
        : base(id)
    {
        if (sourceKind != DataSourceKind.CsvFile)
            throw new DomainException("Only CSV datasets are supported in the MVP.");

        ProjectId = projectId;
        Name = name ?? throw new DomainException("Dataset name is required.");
        SourceKind = sourceKind;
        Description = NormalizeOptionalText(description);
        CreatedAtUtc = now;
        UpdatedAtUtc = now;
    }

    public ProjectId ProjectId { get; }

    public DatasetName Name { get; private set; }

    public string? Description { get; private set; }

    public DataSourceKind SourceKind { get; }

    public DateTimeOffset CreatedAtUtc { get; }

    public DateTimeOffset UpdatedAtUtc { get; private set; }

    public DatasetVersionId? CurrentVersionId { get; private set; }

    public IReadOnlyCollection<DatasetVersion> Versions => _versions.AsReadOnly();

    public static Dataset CreateImportedCsv(
        DatasetId id,
        ProjectId projectId,
        DatasetName name,
        DatasetVersion initialVersion,
        DateTimeOffset now,
        string? description = null)
    {
        if (initialVersion is null)
            throw new DomainException("Imported CSV dataset requires an initial version.");

        Dataset dataset = new(id, projectId, name, DataSourceKind.CsvFile, now, description);
        dataset.AddExistingVersion(initialVersion, now);
        return dataset;
    }

    public void Rename(DatasetName name, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        Name = name ?? throw new DomainException("Dataset name is required.");
        Touch(now);
    }

    public void ChangeDescription(string? description, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        Description = NormalizeOptionalText(description);
        Touch(now);
    }

    public DatasetVersion AddVersion(
        DatasetVersionId versionId,
        DatasetFileReference fileReference,
        DateTimeOffset importedAtUtc,
        long? rowCount = null,
        int? columnCount = null)
    {
        EnsureUpdateTimestamp(importedAtUtc);

        if (_versions.Any(version => version.Id == versionId))
            throw new DomainException("Dataset version ID already exists in this dataset.");

        int nextVersionNumber = _versions.Count == 0
            ? 1
            : _versions.Max(version => version.VersionNumber) + 1;

        DatasetVersion version = new(
            versionId,
            Id,
            nextVersionNumber,
            importedAtUtc,
            fileReference,
            rowCount,
            columnCount);

        AddExistingVersion(version, importedAtUtc);
        return version;
    }

    public void SetCurrentVersion(DatasetVersionId versionId, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);

        if (!_versions.Any(version => version.Id == versionId))
            throw new DomainException("Current dataset version must exist in the dataset.");

        CurrentVersionId = versionId;
        Touch(now);
    }

    private void AddExistingVersion(DatasetVersion version, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);

        if (version is null)
            throw new DomainException("Dataset version is required.");

        if (version.DatasetId != Id)
            throw new DomainException("Dataset version belongs to another dataset.");

        if (_versions.Any(existing => existing.Id == version.Id))
            throw new DomainException("Dataset version ID already exists in this dataset.");

        int expectedVersionNumber = _versions.Count == 0
            ? 1
            : _versions.Max(existing => existing.VersionNumber) + 1;

        if (version.VersionNumber != expectedVersionNumber)
            throw new DomainException("Dataset version numbers must be sequential.");

        if (version.ImportedAtUtc < CreatedAtUtc)
            throw new DomainException("Dataset version import timestamp cannot be before dataset creation.");

        _versions.Add(version);
        CurrentVersionId = version.Id;
        Touch(now);
    }

    private void Touch(DateTimeOffset now) => UpdatedAtUtc = now;

    private void EnsureUpdateTimestamp(DateTimeOffset now)
    {
        if (now < UpdatedAtUtc)
            throw new DomainException("Updated timestamp cannot be before the last dataset update.");
    }

    private static string? NormalizeOptionalText(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
