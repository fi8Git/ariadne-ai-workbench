using Ariadne.Domain.Common;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Tests.Common;

public class EntityTests
{
    [Fact]
    public void EntitiesWithSameTypedIdAreEqual()
    {
        ProjectId id = ProjectId.New();

        var first = new TestEntity(id);
        var second = new TestEntity(id);

        Assert.Equal(first, second);
        Assert.Equal(first.GetHashCode(), second.GetHashCode());
    }

    [Fact]
    public void EntitiesWithDifferentTypedIdsAreNotEqual()
    {
        var first = new TestEntity(ProjectId.New());
        var second = new TestEntity(ProjectId.New());

        Assert.NotEqual(first, second);
    }

    [Fact]
    public void AggregateRootExposesTypedId()
    {
        ProjectId id = ProjectId.New();

        var aggregate = new TestAggregateRoot(id);

        Assert.Equal(id, aggregate.Id);
    }

    private sealed class TestEntity(ProjectId id) : Entity<ProjectId>(id);

    private sealed class TestAggregateRoot(ProjectId id) : AggregateRoot<ProjectId>(id);
}
