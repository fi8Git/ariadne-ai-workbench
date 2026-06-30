using Ariadne.Domain.Common;
using Ariadne.Domain.Projects;

namespace Ariadne.Domain.Tests.Projects;

public class ProjectNameTests
{
    [Fact]
    public void CreateTrimsProjectName()
    {
        var name = new ProjectName("  Housing Study  ");

        Assert.Equal("Housing Study", name.Value);
    }

    [Fact]
    public void ProjectNamesWithSameValueAreEqual()
    {
        Assert.Equal(new ProjectName("Housing Study"), new ProjectName("Housing Study"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void CreateWithMissingValueThrowsDomainException(string? value)
    {
        Assert.Throws<DomainException>(() => new ProjectName(value!));
    }

    [Fact]
    public void CreateWithTooLongValueThrowsDomainException()
    {
        string value = new('A', ProjectName.MaxLength + 1);

        Assert.Throws<DomainException>(() => new ProjectName(value));
    }
}
