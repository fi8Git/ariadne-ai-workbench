using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Datasets;

namespace Ariadne.Domain.Tests.Datasets;

public class DatasetVersionTests
{
    private static readonly DateTimeOffset ImportedAt = new(2026, 6, 29, 10, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset NonUtcImportedAt = new(2026, 6, 29, 10, 0, 0, TimeSpan.FromHours(2));

    [Fact]
    public void CreateWithValidDataStoresVersionMetadata()
    {
        DatasetId datasetId = DatasetId.New();
        DatasetFileReference fileReference = CreateFileReference();
        var warning = new ParsingWarning("Header inferred from first row.");

        var version = new DatasetVersion(
            DatasetVersionId.New(),
            datasetId,
            1,
            ImportedAt,
            fileReference,
            rowCount: 10,
            columnCount: 3,
            parsingWarnings: [warning]);

        Assert.Equal(datasetId, version.DatasetId);
        Assert.Equal(1, version.VersionNumber);
        Assert.Equal(ImportedAt, version.ImportedAtUtc);
        Assert.Equal(fileReference, version.FileReference);
        Assert.Equal(10, version.RowCount);
        Assert.Equal(3, version.ColumnCount);
        Assert.Equal([warning], version.ParsingWarnings);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void CreateWithInvalidVersionNumberThrowsDomainException(int versionNumber)
    {
        Assert.Throws<DomainException>(
            () => CreateVersion(versionNumber: versionNumber));
    }

    [Fact]
    public void CreateWithDefaultVersionIdThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new DatasetVersion(
                default,
                DatasetId.New(),
                1,
                ImportedAt,
                CreateFileReference()));
    }

    [Fact]
    public void CreateWithDefaultDatasetIdThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new DatasetVersion(
                DatasetVersionId.New(),
                default,
                1,
                ImportedAt,
                CreateFileReference()));
    }

    [Fact]
    public void CreateWithNonUtcImportedAtThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new DatasetVersion(
                DatasetVersionId.New(),
                DatasetId.New(),
                1,
                NonUtcImportedAt,
                CreateFileReference()));
    }

    [Fact]
    public void CreateWithMissingFileReferenceThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new DatasetVersion(
                DatasetVersionId.New(),
                DatasetId.New(),
                1,
                ImportedAt,
                null!));
    }

    [Fact]
    public void CreateWithNegativeRowCountThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => CreateVersion(rowCount: -1));
    }

    [Fact]
    public void CreateWithNegativeColumnCountThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => CreateVersion(columnCount: -1));
    }

    [Fact]
    public void ParsingWarningRequiresMessage()
    {
        Assert.Throws<DomainException>(() => new ParsingWarning(" "));
    }

    internal static DatasetFileReference CreateFileReference()
        => new(
            new StorageKey("data/original/version-id/source.csv"),
            "customers.csv",
            new ContentHash("sha256", "abc123"),
            1024,
            "text/csv");

    internal static DatasetVersion CreateVersion(
        DatasetId? datasetId = null,
        int versionNumber = 1,
        long? rowCount = null,
        int? columnCount = null)
        => new(
            DatasetVersionId.New(),
            datasetId ?? DatasetId.New(),
            versionNumber,
            ImportedAt,
            CreateFileReference(),
            rowCount,
            columnCount);
}
