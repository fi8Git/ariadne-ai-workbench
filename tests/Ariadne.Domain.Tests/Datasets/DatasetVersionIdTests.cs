using Ariadne.Domain.Common;
using Ariadne.Domain.Datasets;

namespace Ariadne.Domain.Tests.Datasets;

public class DatasetVersionIdTests
{
    [Fact]
    public void NewCreatesNonEmptyDatasetVersionId()
    {
        DatasetVersionId id = DatasetVersionId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void CreateWithEmptyGuidThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => new DatasetVersionId(Guid.Empty));
    }

    [Fact]
    public void DatasetVersionIdsWithSameValueAreEqual()
    {
        Guid value = Guid.NewGuid();

        Assert.Equal(new DatasetVersionId(value), new DatasetVersionId(value));
    }
}
