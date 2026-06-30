using Ariadne.Domain.Common;
using Ariadne.Domain.Decisions;

namespace Ariadne.Domain.Tests.Decisions;

public class DecisionLogEntryIdTests
{
    [Fact]
    public void NewCreatesNonEmptyDecisionLogEntryId()
    {
        DecisionLogEntryId id = DecisionLogEntryId.New();

        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void CreateWithEmptyGuidThrowsDomainException()
    {
        Assert.Throws<DomainException>(() => new DecisionLogEntryId(Guid.Empty));
    }

    [Fact]
    public void DecisionLogEntryIdsWithSameValueAreEqual()
    {
        Guid value = Guid.NewGuid();

        Assert.Equal(new DecisionLogEntryId(value), new DecisionLogEntryId(value));
    }
}
