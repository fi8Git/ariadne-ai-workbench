using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Datasets;

namespace Ariadne.Domain.Tests.Datasets;

public class DatasetFileReferenceTests
{
    [Fact]
    public void CreateWithValidDataStoresFileMetadata()
    {
        var hash = new ContentHash("sha256", "abc123");

        var reference = new DatasetFileReference(
            new StorageKey("data/original/version-id/source.csv"),
            " customers.csv ",
            hash,
            1024,
            " text/csv ");

        Assert.Equal("data/original/version-id/source.csv", reference.StorageKey.Value);
        Assert.Equal("customers.csv", reference.OriginalFileName);
        Assert.Equal(hash, reference.ContentHash);
        Assert.Equal(1024, reference.SizeInBytes);
        Assert.Equal("text/csv", reference.MediaType);
    }

    [Fact]
    public void CreateNormalizesBlankMediaTypeToNull()
    {
        var reference = CreateReference(mediaType: " ");

        Assert.Null(reference.MediaType);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateWithMissingOriginalFileNameThrowsDomainException(string? originalFileName)
    {
        Assert.Throws<DomainException>(
            () => new DatasetFileReference(
                new StorageKey("data/original/version-id/source.csv"),
                originalFileName!));
    }

    [Fact]
    public void CreateWithMissingStorageKeyThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new DatasetFileReference(null!, "customers.csv"));
    }

    [Fact]
    public void CreateWithNegativeSizeThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => CreateReference(sizeInBytes: -1));
    }

    private static DatasetFileReference CreateReference(long? sizeInBytes = null, string? mediaType = null)
        => new(
            new StorageKey("data/original/version-id/source.csv"),
            "customers.csv",
            new ContentHash("sha256", "abc123"),
            sizeInBytes,
            mediaType);
}
