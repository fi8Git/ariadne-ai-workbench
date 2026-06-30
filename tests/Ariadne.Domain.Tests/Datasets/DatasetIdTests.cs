using Ariadne.Domain.Common;
using Ariadne.Domain.Datasets;

namespace Ariadne.Domain.Tests.Datasets;

public class DatasetIdTests
{
    [Fact]
    public void NewCreatesNonEmptyDatasetId()
    {
        DatasetId id = DatasetId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void CreateWithEmptyGuidThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => new DatasetId(Guid.Empty));
    }

    [Fact]
    public void DatasetIdsWithSameValueAreEqual()
    {
        Guid value = Guid.NewGuid();

        Assert.Equal(new DatasetId(value), new DatasetId(value));
    }
}
