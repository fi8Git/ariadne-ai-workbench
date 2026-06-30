using System.Globalization;
using Ariadne.Domain.Common;

namespace Ariadne.Domain.Decisions;

public readonly record struct DecisionLogEntryId
{
    public DecisionLogEntryId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Decision log entry ID is required.");

        Value = value;
    }

    public Guid Value { get; }

    public static DecisionLogEntryId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("D", CultureInfo.InvariantCulture);
}
