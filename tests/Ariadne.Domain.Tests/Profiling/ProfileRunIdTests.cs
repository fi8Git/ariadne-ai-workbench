using Ariadne.Domain.Common;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class ProfileRunIdTests
{
    [Fact]
    public void NewCreatesNonEmptyProfileRunId()
    {
        ProfileRunId id = ProfileRunId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void CreateWithEmptyGuidThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => new ProfileRunId(Guid.Empty));
    }

    [Fact]
    public void ProfileRunIdsWithSameValueAreEqual()
    {
        Guid value = Guid.NewGuid();

        Assert.Equal(new ProfileRunId(value), new ProfileRunId(value));
    }
}
