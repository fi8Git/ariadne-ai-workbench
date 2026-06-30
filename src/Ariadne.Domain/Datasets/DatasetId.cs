using System.Globalization;
using Ariadne.Domain.Common;

namespace Ariadne.Domain.Datasets;

public readonly record struct DatasetId
{
    public DatasetId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Dataset ID is required.");

        Value = value;
    }

    public Guid Value { get; }

    public static DatasetId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString("D", CultureInfo.InvariantCulture);
}
