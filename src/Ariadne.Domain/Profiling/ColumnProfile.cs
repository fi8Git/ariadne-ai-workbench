using Ariadne.Domain.Common;

namespace Ariadne.Domain.Profiling;

public sealed class ColumnProfile : Entity<ColumnName>
{
    public ColumnProfile(
        ColumnName columnName,
        InferredValue<PrimitiveDataType> inferredPrimitiveType,
        InferredValue<MethodologicalVariableType> inferredMethodologicalType,
        MissingValueSummary missing,
        DistinctValueSummary distinct,
        NumericColumnStatistics? numericStatistics = null,
        OutlierCandidateSummary? outliers = null,
        IEnumerable<ValueCount>? topValueCounts = null,
        IEnumerable<string>? sampleValues = null,
        IEnumerable<string>? warnings = null)
        : base(columnName ?? throw new DomainException("Column name is required."))
    {
        InferredPrimitiveType = inferredPrimitiveType
            ?? throw new DomainException("Inferred primitive type is required.");

        InferredMethodologicalType = inferredMethodologicalType
            ?? throw new DomainException("Inferred methodological type is required.");

        Missing = missing
            ?? throw new DomainException("Missing value summary is required.");

        Distinct = distinct
            ?? throw new DomainException("Distinct value summary is required.");

        if (!IsNumeric(InferredPrimitiveType.Value) && numericStatistics is not null)
            throw new DomainException("Numeric statistics require a numeric primitive type.");

        if (!IsNumeric(InferredPrimitiveType.Value) && outliers is not null)
            throw new DomainException("Outlier candidates require a numeric primitive type.");

        NumericStatistics = numericStatistics;
        Outliers = outliers;
        TopValueCounts = ValidateTopValueCounts(topValueCounts);
        SampleValues = ValidateTextValues(sampleValues, "Sample values must not contain null entries.");
        Warnings = ValidateTextValues(warnings, "Column profile warnings must not contain null entries.");
    }

    public ColumnName ColumnName => Id;

    public InferredValue<PrimitiveDataType> InferredPrimitiveType { get; }

    public InferredValue<MethodologicalVariableType> InferredMethodologicalType { get; }

    public MissingValueSummary Missing { get; }

    public DistinctValueSummary Distinct { get; }

    public NumericColumnStatistics? NumericStatistics { get; }

    public OutlierCandidateSummary? Outliers { get; }

    public IReadOnlyCollection<ValueCount> TopValueCounts { get; }

    public IReadOnlyCollection<string> SampleValues { get; }

    public IReadOnlyCollection<string> Warnings { get; }

    private static bool IsNumeric(PrimitiveDataType primitiveType)
        => primitiveType is PrimitiveDataType.Integer or PrimitiveDataType.Decimal;

    private static ValueCount[] ValidateTopValueCounts(IEnumerable<ValueCount>? valueCounts)
    {
        ValueCount[] values = valueCounts?.ToArray() ?? [];

        if (values.Any(valueCount => valueCount is null))
            throw new DomainException("Top value counts must not contain null entries.");

        return values;
    }

    private static string[] ValidateTextValues(IEnumerable<string>? values, string errorMessage)
    {
        string[] normalized = values?.Select(value =>
        {
            if (value is null)
                throw new DomainException(errorMessage);

            return value;
        }).ToArray() ?? [];

        return normalized;
    }
}
