using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;

namespace Ariadne.Domain.Tests.Common;

public class ContentHashTests
{
    [Fact]
    public void CreateTrimsValueAndNormalizesAlgorithm()
    {
        var hash = new ContentHash(" sha256 ", " abc123 ");

        Assert.Equal(ContentHash.Sha256Algorithm, hash.Algorithm);
        Assert.Equal("abc123", hash.Value);
    }

    [Fact]
    public void HashesWithSameAlgorithmAndValueAreEqual()
    {
        Assert.Equal(
            new ContentHash("sha256", "abc123"),
            new ContentHash("SHA256", "abc123"));
    }

    [Theory]
    [InlineData(null, "abc123")]
    [InlineData("", "abc123")]
    [InlineData(" ", "abc123")]
    [InlineData("MD5", "abc123")]
    [InlineData("SHA256", null)]
    [InlineData("SHA256", "")]
    [InlineData("SHA256", " ")]
    public void CreateWithInvalidPartsThrowsDomainException(string? algorithm, string? value)
    {
        Assert.Throws<DomainException>(() => new ContentHash(algorithm!, value!));
    }
}
