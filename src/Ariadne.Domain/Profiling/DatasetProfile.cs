using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Datasets;

namespace Ariadne.Domain.Profiling;

public sealed class DatasetProfile : AggregateRoot<ProfileRunId>
{
    private DatasetProfile(
        ProfileRunId id,
        DatasetVersionId datasetVersionId,
        DateTimeOffset createdAtUtc,
        long rowCount,
        int columnCount,
        long totalMissingCells,
        Ratio missingCellRatio,
        IReadOnlyCollection<ColumnProfile> columns,
        IReadOnlyCollection<ParsingWarning> warnings,
        long? duplicateRowCount)
        : base(id)
    {
        DatasetVersionId = datasetVersionId;
        CreatedAtUtc = createdAtUtc;
        RowCount = rowCount;
        ColumnCount = columnCount;
        TotalMissingCells = totalMissingCells;
        MissingCellRatio = missingCellRatio;
        Columns = columns;
        Warnings = warnings;
        DuplicateRowCount = duplicateRowCount;
    }

    public DatasetVersionId DatasetVersionId { get; }

    public DateTimeOffset CreatedAtUtc { get; }

    public long RowCount { get; }

    public int ColumnCount { get; }

    public long TotalMissingCells { get; }

    public Ratio MissingCellRatio { get; }

    public long? DuplicateRowCount { get; }

    public IReadOnlyCollection<ColumnProfile> Columns { get; }

    public IReadOnlyCollection<ParsingWarning> Warnings { get; }

    public static DatasetProfile Create(
        ProfileRunId id,
        DatasetVersionId datasetVersionId,
        DateTimeOffset createdAtUtc,
        long rowCount,
        int columnCount,
        long totalMissingCells,
        Ratio missingCellRatio,
        IEnumerable<ColumnProfile> columns,
        IEnumerable<ParsingWarning>? warnings = null,
        long? duplicateRowCount = null)
    {
        if (rowCount < 0)
            throw new DomainException("Dataset profile row count must be non-negative.");

        if (columnCount < 0)
            throw new DomainException("Dataset profile column count must be non-negative.");

        if (totalMissingCells < 0)
            throw new DomainException("Dataset profile missing cell count must be non-negative.");

        if (duplicateRowCount < 0)
            throw new DomainException("Duplicate row count must be non-negative.");

        ColumnProfile[] columnArray = columns?.ToArray()
            ?? throw new DomainException("Dataset profile columns are required.");

        if (columnArray.Any(column => column is null))
            throw new DomainException("Dataset profile columns must not contain null entries.");

        if (columnArray.Length != columnCount)
            throw new DomainException("Dataset profile column count must match the number of columns.");

        ColumnName[] duplicateNames = [.. columnArray
            .GroupBy(column => column.ColumnName)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)];

        if (duplicateNames.Length > 0)
            throw new DomainException("Dataset profile column names must be unique.");

        decimal totalCellCount = (decimal)rowCount * columnCount;

        if (totalCellCount > 0 && totalMissingCells > totalCellCount)
            throw new DomainException("Dataset profile missing cell count cannot exceed total cell count.");

        ParsingWarning[] warningArray = warnings?.ToArray() ?? [];

        if (warningArray.Any(warning => warning is null))
            throw new DomainException("Dataset profile warnings must not contain null entries.");

        return new DatasetProfile(
            id,
            datasetVersionId,
            createdAtUtc,
            rowCount,
            columnCount,
            totalMissingCells,
            missingCellRatio,
            columnArray,
            warningArray,
            duplicateRowCount);
    }
}
