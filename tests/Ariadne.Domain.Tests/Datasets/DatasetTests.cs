using Ariadne.Domain.Common;
using Ariadne.Domain.Datasets;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Tests.Datasets;

public class DatasetTests
{
    private static readonly DateTimeOffset CreatedAt = new(2026, 6, 29, 10, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset NonUtc = new(2026, 6, 29, 10, 0, 0, TimeSpan.FromHours(2));

    [Fact]
    public void CreateImportedCsvWithInitialVersionSucceeds()
    {
        DatasetId datasetId = DatasetId.New();
        ProjectId projectId = ProjectId.New();
        DatasetVersion initialVersion = CreateVersion(datasetId, 1, CreatedAt);

        Dataset dataset = Dataset.CreateImportedCsv(
            datasetId,
            projectId,
            new DatasetName("  Customers  "),
            initialVersion,
            CreatedAt,
            "  Imported CRM export  ");

        Assert.Equal(datasetId, dataset.Id);
        Assert.Equal(projectId, dataset.ProjectId);
        Assert.Equal("Customers", dataset.Name.Value);
        Assert.Equal("Imported CRM export", dataset.Description);
        Assert.Equal(DataSourceKind.CsvFile, dataset.SourceKind);
        Assert.Equal(CreatedAt, dataset.CreatedAtUtc);
        Assert.Equal(CreatedAt, dataset.UpdatedAtUtc);
        Assert.Equal(initialVersion.Id, dataset.CurrentVersionId);
        Assert.Equal([initialVersion], dataset.Versions);
    }

    [Fact]
    public void CreateImportedCsvWithInvalidNameThrowsDomainException()
    {
        DatasetId datasetId = DatasetId.New();

        Assert.Throws<DomainException>(
            () => Dataset.CreateImportedCsv(
                datasetId,
                ProjectId.New(),
                new DatasetName(" "),
                CreateVersion(datasetId, 1, CreatedAt),
                CreatedAt));
    }

    [Fact]
    public void CreateImportedCsvWithDefaultDatasetIdThrowsDomainException()
    {
        DatasetId initialVersionDatasetId = DatasetId.New();

        Assert.Throws<DomainException>(
            () => Dataset.CreateImportedCsv(
                default,
                ProjectId.New(),
                new DatasetName("Customers"),
                CreateVersion(initialVersionDatasetId, 1, CreatedAt),
                CreatedAt));
    }

    [Fact]
    public void CreateImportedCsvWithDefaultProjectIdThrowsDomainException()
    {
        DatasetId datasetId = DatasetId.New();

        Assert.Throws<DomainException>(
            () => Dataset.CreateImportedCsv(
                datasetId,
                default,
                new DatasetName("Customers"),
                CreateVersion(datasetId, 1, CreatedAt),
                CreatedAt));
    }

    [Fact]
    public void CreateImportedCsvWithNonUtcTimestampThrowsDomainException()
    {
        DatasetId datasetId = DatasetId.New();

        Assert.Throws<DomainException>(
            () => Dataset.CreateImportedCsv(
                datasetId,
                ProjectId.New(),
                new DatasetName("Customers"),
                CreateVersion(datasetId, 1, CreatedAt),
                NonUtc));
    }

    [Fact]
    public void CreateImportedCsvWithoutInitialVersionThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => Dataset.CreateImportedCsv(
                DatasetId.New(),
                ProjectId.New(),
                new DatasetName("Customers"),
                null!,
                CreatedAt));
    }

    [Fact]
    public void CreateImportedCsvRejectsVersionFromAnotherDataset()
    {
        Assert.Throws<DomainException>(
            () => Dataset.CreateImportedCsv(
                DatasetId.New(),
                ProjectId.New(),
                new DatasetName("Customers"),
                CreateVersion(DatasetId.New(), 1, CreatedAt),
                CreatedAt));
    }

    [Fact]
    public void CreateImportedCsvRequiresFirstVersionNumber()
    {
        DatasetId datasetId = DatasetId.New();

        Assert.Throws<DomainException>(
            () => Dataset.CreateImportedCsv(
                datasetId,
                ProjectId.New(),
                new DatasetName("Customers"),
                CreateVersion(datasetId, 2, CreatedAt),
                CreatedAt));
    }

    [Fact]
    public void AddVersionCreatesNextSequentialVersionAndSetsCurrent()
    {
        DatasetId datasetId = DatasetId.New();
        Dataset dataset = CreateDataset(datasetId);
        DateTimeOffset importedAt = CreatedAt.AddMinutes(10);

        DatasetVersion secondVersion = dataset.AddVersion(
            DatasetVersionId.New(),
            DatasetVersionTests.CreateFileReference(),
            importedAt,
            rowCount: 20,
            columnCount: 4);

        Assert.Equal(2, secondVersion.VersionNumber);
        Assert.Equal(datasetId, secondVersion.DatasetId);
        Assert.Equal(20, secondVersion.RowCount);
        Assert.Equal(4, secondVersion.ColumnCount);
        Assert.Equal(secondVersion.Id, dataset.CurrentVersionId);
        Assert.Equal(importedAt, dataset.UpdatedAtUtc);
        Assert.Equal(2, dataset.Versions.Count);
    }

    [Fact]
    public void AddVersionRejectsDuplicateVersionId()
    {
        DatasetVersionId versionId = DatasetVersionId.New();
        Dataset dataset = CreateDataset();

        dataset.AddVersion(versionId, DatasetVersionTests.CreateFileReference(), CreatedAt.AddMinutes(10));

        Assert.Throws<DomainException>(
            () => dataset.AddVersion(versionId, DatasetVersionTests.CreateFileReference(), CreatedAt.AddMinutes(11)));
    }

    [Fact]
    public void AddVersionWithDefaultVersionIdThrowsDomainException()
    {
        Dataset dataset = CreateDataset();

        Assert.Throws<DomainException>(
            () => dataset.AddVersion(default, DatasetVersionTests.CreateFileReference(), CreatedAt.AddMinutes(10)));
    }

    [Fact]
    public void AddVersionWithNonUtcTimestampThrowsDomainException()
    {
        Dataset dataset = CreateDataset();

        Assert.Throws<DomainException>(
            () => dataset.AddVersion(
                DatasetVersionId.New(),
                DatasetVersionTests.CreateFileReference(),
                NonUtc.AddMinutes(10)));
    }

    [Fact]
    public void SetCurrentVersionRequiresKnownVersion()
    {
        Dataset dataset = CreateDataset();

        Assert.Throws<DomainException>(
            () => dataset.SetCurrentVersion(DatasetVersionId.New(), CreatedAt.AddMinutes(10)));
    }

    [Fact]
    public void SetCurrentVersionWithDefaultVersionIdThrowsDomainException()
    {
        Dataset dataset = CreateDataset();

        Assert.Throws<DomainException>(() => dataset.SetCurrentVersion(default, CreatedAt.AddMinutes(10)));
    }

    [Fact]
    public void SetCurrentVersionUpdatesCurrentVersionAndTimestamp()
    {
        Dataset dataset = CreateDataset();
        DatasetVersion secondVersion = dataset.AddVersion(
            DatasetVersionId.New(),
            DatasetVersionTests.CreateFileReference(),
            CreatedAt.AddMinutes(10));
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(20);

        dataset.SetCurrentVersion(secondVersion.Id, updatedAt);

        Assert.Equal(secondVersion.Id, dataset.CurrentVersionId);
        Assert.Equal(updatedAt, dataset.UpdatedAtUtc);
    }

    [Fact]
    public void RenameUpdatesNameAndTimestamp()
    {
        Dataset dataset = CreateDataset();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(10);

        dataset.Rename(new DatasetName("Updated Customers"), updatedAt);

        Assert.Equal("Updated Customers", dataset.Name.Value);
        Assert.Equal(updatedAt, dataset.UpdatedAtUtc);
    }

    [Fact]
    public void ChangeDescriptionNormalizesBlankTextToNull()
    {
        Dataset dataset = CreateDataset();

        dataset.ChangeDescription(" ", CreatedAt.AddMinutes(10));

        Assert.Null(dataset.Description);
    }

    [Fact]
    public void UpdateBeforeLastUpdateThrowsDomainException()
    {
        Dataset dataset = CreateDataset();
        dataset.Rename(new DatasetName("Updated"), CreatedAt.AddMinutes(10));

        Assert.Throws<DomainException>(
            () => dataset.Rename(new DatasetName("Older"), CreatedAt.AddMinutes(5)));

        Assert.Equal("Updated", dataset.Name.Value);
    }

    [Fact]
    public void UpdateWithNonUtcTimestampThrowsDomainException()
    {
        Dataset dataset = CreateDataset();

        Assert.Throws<DomainException>(
            () => dataset.Rename(new DatasetName("Updated"), NonUtc.AddMinutes(10)));

        Assert.Equal("Customers", dataset.Name.Value);
    }

    private static Dataset CreateDataset(DatasetId? datasetId = null)
    {
        DatasetId id = datasetId ?? DatasetId.New();

        return Dataset.CreateImportedCsv(
            id,
            ProjectId.New(),
            new DatasetName("Customers"),
            CreateVersion(id, 1, CreatedAt),
            CreatedAt);
    }

    private static DatasetVersion CreateVersion(
        DatasetId datasetId,
        int versionNumber,
        DateTimeOffset importedAtUtc)
        => new(
            DatasetVersionId.New(),
            datasetId,
            versionNumber,
            importedAtUtc,
            DatasetVersionTests.CreateFileReference());
}
