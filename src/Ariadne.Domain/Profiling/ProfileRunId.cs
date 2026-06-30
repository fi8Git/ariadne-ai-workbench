using System.Globalization;
using Ariadne.Domain.Common;

namespace Ariadne.Domain.Profiling;

public readonly record struct ProfileRunId
{
    public ProfileRunId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Profile run ID is required.");

        Value = value;
    }

    public Guid Value { get; }

    public static ProfileRunId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("D", CultureInfo.InvariantCulture);
}
