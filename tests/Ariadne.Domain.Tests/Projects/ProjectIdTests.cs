using Ariadne.Domain.Common;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Tests.Projects;

public class ProjectIdTests
{
    [Fact]
    public void NewCreatesNonEmptyProjectId()
    {
        ProjectId id = ProjectId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void CreateWithEmptyGuidThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => new ProjectId(Guid.Empty));
    }

    [Fact]
    public void ProjectIdsWithSameValueAreEqual()
    {
        Guid value = Guid.NewGuid();

        Assert.Equal(new ProjectId(value), new ProjectId(value));
    }
}
