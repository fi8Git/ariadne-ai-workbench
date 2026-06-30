using System.Globalization;
using Ariadne.Domain.Common;

namespace Ariadne.Domain.Projects;

public readonly record struct ProjectId
{
    public ProjectId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Project ID is required.");

        Value = value;
    }

    public Guid Value { get; }

    public static ProjectId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("D", CultureInfo.InvariantCulture);
}
