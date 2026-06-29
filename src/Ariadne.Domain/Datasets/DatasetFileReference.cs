using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;

namespace Ariadne.Domain.Datasets;

public sealed record DatasetFileReference
{
    public DatasetFileReference(
        StorageKey storageKey,
        string originalFileName,
        ContentHash? contentHash = null,
        long? sizeInBytes = null,
        string? mediaType = null)
    {
        if (sizeInBytes < 0)
            throw new DomainException("Dataset file size must be non-negative.");

        StorageKey = storageKey ?? throw new DomainException("Dataset file storage key is required.");

        if (string.IsNullOrWhiteSpace(originalFileName))
            throw new DomainException("Original file name is required.");

        OriginalFileName = originalFileName.Trim();

        ContentHash = contentHash;
        SizeInBytes = sizeInBytes;
        MediaType = NormalizeOptionalText(mediaType);
    }

    public StorageKey StorageKey { get; }

    public string OriginalFileName { get; }

    public ContentHash? ContentHash { get; }

    public long? SizeInBytes { get; }

    public string? MediaType { get; }

    private static string? NormalizeOptionalText(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
