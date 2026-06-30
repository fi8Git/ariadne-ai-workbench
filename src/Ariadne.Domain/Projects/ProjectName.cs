using Ariadne.Domain.Common;

namespace Ariadne.Domain.Projects;

public sealed record ProjectName
{
    public const int MaxLength = 200;

    public ProjectName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Project name is required.");

        Value = value.Trim();

        if (Value.Length > MaxLength)
            throw new DomainException("Project name must be 200 characters or fewer.");
    }

    public string Value { get; }

    public override string ToString() => Value;
}
