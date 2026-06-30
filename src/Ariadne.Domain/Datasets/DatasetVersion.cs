using Ariadne.Domain.Common;

namespace Ariadne.Domain.Datasets;

public sealed class DatasetVersion : Entity<DatasetVersionId>
{
    public DatasetVersion(
        DatasetVersionId id,
        DatasetId datasetId,
        int versionNumber,
        DateTimeOffset importedAtUtc,
        DatasetFileReference fileReference,
        long? rowCount = null,
        int? columnCount = null,
        IEnumerable<ParsingWarning>? parsingWarnings = null)
        : base(DomainGuard.EnsureNotDefaultId(id, id.Value, "Dataset version ID is required."))
    {
        DomainGuard.EnsureNotDefaultId(datasetId, datasetId.Value, "Dataset ID is required.");
        importedAtUtc = DomainGuard.EnsureUtc(importedAtUtc, nameof(importedAtUtc));

        if (versionNumber <= 0)
            throw new DomainException("Dataset version number must be greater than zero.");

        if (rowCount < 0)
            throw new DomainException("Dataset row count must be non-negative.");

        if (columnCount < 0)
            throw new DomainException("Dataset column count must be non-negative.");

        ParsingWarning[] warnings = parsingWarnings?.ToArray() ?? [];

        if (warnings.Any(warning => warning is null))
            throw new DomainException("Parsing warnings must not contain null entries.");

        DatasetId = datasetId;
        VersionNumber = versionNumber;
        ImportedAtUtc = importedAtUtc;
        FileReference = fileReference ?? throw new DomainException("Dataset file reference is required.");
        RowCount = rowCount;
        ColumnCount = columnCount;
        ParsingWarnings = warnings;
    }

    public DatasetId DatasetId { get; }

    public int VersionNumber { get; }

    public DateTimeOffset ImportedAtUtc { get; }

    public DatasetFileReference FileReference { get; }

    public long? RowCount { get; }

    public int? ColumnCount { get; }

    public IReadOnlyCollection<ParsingWarning> ParsingWarnings { get; }
}
