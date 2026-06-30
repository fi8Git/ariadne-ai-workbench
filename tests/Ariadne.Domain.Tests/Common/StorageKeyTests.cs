using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;

namespace Ariadne.Domain.Tests.Common;

public class StorageKeyTests
{
    [Fact]
    public void CreateTrimsStorageKey()
    {
        StorageKey key = new("  data/original/version-id/source.csv  ");

        Assert.Equal("data/original/version-id/source.csv", key.Value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("/data/original/source.csv")]
    [InlineData("C:/data/source.csv")]
    [InlineData("data\\original\\source.csv")]
    [InlineData("data//source.csv")]
    [InlineData("data/./source.csv")]
    [InlineData("data/../source.csv")]
    [InlineData("data/source.csv/")]
    public void CreateWithInvalidValueThrowsDomainException(string? value)
    {
        Assert.Throws<DomainException>(() => new StorageKey(value!));
    }

    [Fact]
    public void StorageKeysWithSameValueAreEqual()
    {
        Assert.Equal(
            new StorageKey("data/original/source.csv"),
            new StorageKey("data/original/source.csv"));
    }
}
