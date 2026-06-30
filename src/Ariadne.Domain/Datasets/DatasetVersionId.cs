using System.Globalization;
using Ariadne.Domain.Common;

namespace Ariadne.Domain.Datasets;

public readonly record struct DatasetVersionId
{
    public DatasetVersionId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Dataset version ID is required.");

        Value = value;
    }

    public Guid Value { get; }

    public static DatasetVersionId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("D", CultureInfo.InvariantCulture);
}
