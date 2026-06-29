using Ariadne.Domain.Common;
using Ariadne.Domain.Datasets;

namespace Ariadne.Domain.Tests.Datasets;

public class DatasetNameTests
{
    [Fact]
    public void CreateTrimsDatasetName()
    {
        var name = new DatasetName("  Training Data  ");

        Assert.Equal("Training Data", name.Value);
    }

    [Fact]
    public void DatasetNamesWithSameValueAreEqual()
    {
        Assert.Equal(new DatasetName("Training Data"), new DatasetName("Training Data"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateWithMissingValueThrowsDomainException(string? value)
    {
        Assert.Throws<DomainException>(() => new DatasetName(value!));
    }

    [Fact]
    public void CreateWithTooLongValueThrowsDomainException()
    {
        string value = new('A', DatasetName.MaxLength + 1);

        Assert.Throws<DomainException>(() => new DatasetName(value));
    }
}
