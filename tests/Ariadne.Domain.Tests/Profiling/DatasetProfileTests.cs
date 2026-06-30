using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Datasets;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class DatasetProfileTests
{
    private static readonly DateTimeOffset CreatedAt = new(2026, 6, 29, 14, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset NonUtcCreatedAt = new(2026, 6, 29, 14, 0, 0, TimeSpan.FromHours(2));

    [Fact]
    public void CreateWithValidDataStoresProfileMetadata()
    {
        DatasetVersionId versionId = DatasetVersionId.New();
        ColumnProfile price = ColumnProfileTests.CreateNumericColumn();
        ColumnProfile city = ColumnProfileTests.CreateTextColumn();
        var warning = new ParsingWarning("Some rows had blank cells.");

        DatasetProfile profile = DatasetProfile.Create(
            ProfileRunId.New(),
            versionId,
            CreatedAt,
            rowCount: 10,
            columnCount: 2,
            totalMissingCells: 1,
            missingCellRatio: new Ratio(0.05),
            columns: [price, city],
            warnings: [warning],
            duplicateRowCount: 0);

        Assert.Equal(versionId, profile.DatasetVersionId);
        Assert.Equal(CreatedAt, profile.CreatedAtUtc);
        Assert.Equal(10, profile.RowCount);
        Assert.Equal(2, profile.ColumnCount);
        Assert.Equal(1, profile.TotalMissingCells);
        Assert.Equal(0.05, profile.MissingCellRatio.Value);
        Assert.Equal(0, profile.DuplicateRowCount);
        Assert.Equal([price, city], profile.Columns);
        Assert.Equal([warning], profile.Warnings);
    }

    [Fact]
    public void CreateRejectsNegativeRowCount()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(rowCount: -1));
    }

    [Fact]
    public void CreateRejectsDefaultProfileRunId()
    {
        Assert.Throws<DomainException>(
            () => DatasetProfile.Create(
                default,
                DatasetVersionId.New(),
                CreatedAt,
                rowCount: 1,
                columnCount: 1,
                totalMissingCells: 0,
                missingCellRatio: Ratio.Zero,
                columns: [ColumnProfileTests.CreateTextColumn()]));
    }

    [Fact]
    public void CreateRejectsDefaultDatasetVersionId()
    {
        Assert.Throws<DomainException>(
            () => DatasetProfile.Create(
                ProfileRunId.New(),
                default,
                CreatedAt,
                rowCount: 1,
                columnCount: 1,
                totalMissingCells: 0,
                missingCellRatio: Ratio.Zero,
                columns: [ColumnProfileTests.CreateTextColumn()]));
    }

    [Fact]
    public void CreateRejectsNonUtcTimestamp()
    {
        Assert.Throws<DomainException>(
            () => DatasetProfile.Create(
                ProfileRunId.New(),
                DatasetVersionId.New(),
                NonUtcCreatedAt,
                rowCount: 1,
                columnCount: 1,
                totalMissingCells: 0,
                missingCellRatio: Ratio.Zero,
                columns: [ColumnProfileTests.CreateTextColumn()]));
    }

    [Fact]
    public void CreateRejectsNegativeColumnCount()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(columnCount: -1));
    }

    [Fact]
    public void CreateRejectsNegativeMissingCellCount()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(totalMissingCells: -1));
    }

    [Fact]
    public void CreateRejectsInvalidMissingRatio()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(missingCellRatio: new Ratio(1.1)));
    }

    [Fact]
    public void CreateRejectsNegativeDuplicateRowCount()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(duplicateRowCount: -1));
    }

    [Fact]
    public void CreateRejectsColumnCountMismatch()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(columnCount: 2, columns: [ColumnProfileTests.CreateTextColumn()]));
    }

    [Fact]
    public void CreateRejectsDuplicateColumnNames()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(
                columnCount: 2,
                columns:
                [
                    ColumnProfileTests.CreateTextColumn(new ColumnName("city")),
                    ColumnProfileTests.CreateTextColumn(new ColumnName("city")),
                ]));
    }

    [Fact]
    public void CreateRejectsMissingCellsGreaterThanTotalCells()
    {
        Assert.Throws<DomainException>(
            () => CreateProfile(rowCount: 1, columnCount: 1, totalMissingCells: 2));
    }

    [Fact]
    public void CreateRecalculatesMissingCellRatioFromCounts()
    {
        DatasetProfile profile = CreateProfile(
            rowCount: 10,
            columnCount: 2,
            totalMissingCells: 5,
            missingCellRatio: Ratio.Zero,
            columns:
            [
                ColumnProfileTests.CreateTextColumn(new ColumnName("city")),
                ColumnProfileTests.CreateNumericColumn(new ColumnName("price")),
            ]);

        Assert.Equal(0.25, profile.MissingCellRatio.Value);
    }

    [Fact]
    public void CreateWithNoCellsStoresZeroMissingCellRatio()
    {
        DatasetProfile profile = DatasetProfile.Create(
            ProfileRunId.New(),
            DatasetVersionId.New(),
            CreatedAt,
            rowCount: 0,
            columnCount: 0,
            totalMissingCells: 0,
            missingCellRatio: Ratio.One,
            columns: []);

        Assert.Equal(Ratio.Zero, profile.MissingCellRatio);
    }

    [Fact]
    public void CreateRejectsMissingCellsWhenDatasetHasNoCells()
    {
        Assert.Throws<DomainException>(
            () => DatasetProfile.Create(
                ProfileRunId.New(),
                DatasetVersionId.New(),
                CreatedAt,
                rowCount: 0,
                columnCount: 0,
                totalMissingCells: 1,
                missingCellRatio: Ratio.Zero,
                columns: []));
    }

    [Fact]
    public void CreateRejectsNullColumnCollection()
    {
        Assert.Throws<DomainException>(
            () => DatasetProfile.Create(
                ProfileRunId.New(),
                DatasetVersionId.New(),
                CreatedAt,
                rowCount: 0,
                columnCount: 0,
                totalMissingCells: 0,
                missingCellRatio: Ratio.Zero,
                columns: null!));
    }

    private static DatasetProfile CreateProfile(
        long rowCount = 10,
        int columnCount = 1,
        long totalMissingCells = 1,
        Ratio? missingCellRatio = null,
        IEnumerable<ColumnProfile>? columns = null,
        long? duplicateRowCount = null)
        => DatasetProfile.Create(
            ProfileRunId.New(),
            DatasetVersionId.New(),
            CreatedAt,
            rowCount,
            columnCount,
            totalMissingCells,
            missingCellRatio ?? new Ratio(0.1),
            columns ?? [ColumnProfileTests.CreateTextColumn()],
            duplicateRowCount: duplicateRowCount);
}
