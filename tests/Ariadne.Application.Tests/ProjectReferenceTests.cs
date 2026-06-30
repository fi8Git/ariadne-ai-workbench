using System.Xml.Linq;

namespace Ariadne.Application.Tests;

public class ProjectReferenceTests
{
    [Fact]
    public void ApplicationProjectDependsOnlyOnDomain()
    {
        string projectPath = FindRepositoryFile("src", "Ariadne.Application", "Ariadne.Application.csproj");

        Assert.Equal(
            ["..\\Ariadne.Domain\\Ariadne.Domain.csproj"],
            ReadProjectReferences(projectPath));
    }

    private static string FindRepositoryFile(params string[] relativeParts)
    {
        DirectoryInfo? directory = new(AppContext.BaseDirectory);

        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "Ariadne.sln")))
        {
            directory = directory.Parent;
        }

        Assert.NotNull(directory);
        return Path.Combine(directory!.FullName, Path.Combine(relativeParts));
    }

    private static string[] ReadProjectReferences(string projectPath)
    {
        XDocument document = XDocument.Load(projectPath);

        return document
            .Descendants("ProjectReference")
            .Select(reference => reference.Attribute("Include")?.Value)
            .Where(include => !string.IsNullOrWhiteSpace(include))
            .Select(include => include!.Replace('/', '\\'))
            .OrderBy(include => include, StringComparer.OrdinalIgnoreCase)
            .ToArray();
    }
}
