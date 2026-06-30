using Ariadne.Domain.Common;
using Ariadne.Domain.Common.ValueObjects;
using Ariadne.Domain.Profiling;

namespace Ariadne.Domain.Tests.Profiling;

public class InferredValueTests
{
    [Fact]
    public void CreateStoresInferenceMetadata()
    {
        var inference = new InferredValue<PrimitiveDataType>(
            PrimitiveDataType.Decimal,
            new Ratio(0.8),
            " numeric sample ",
            needsReview: true);

        Assert.Equal(PrimitiveDataType.Decimal, inference.Value);
        Assert.Equal(0.8, inference.Confidence.Value);
        Assert.Equal("numeric sample", inference.Reason);
        Assert.True(inference.NeedsReview);
    }

    [Fact]
    public void CreateWithBlankReasonStoresNull()
    {
        var inference = new InferredValue<PrimitiveDataType>(
            PrimitiveDataType.Text,
            Ratio.One,
            " ",
            needsReview: false);

        Assert.Null(inference.Reason);
    }

    [Fact]
    public void CreateWithInvalidConfidenceThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new InferredValue<PrimitiveDataType>(
                PrimitiveDataType.Text,
                new Ratio(1.1),
                null,
                needsReview: false));
    }

    [Fact]
    public void CreateWithUndefinedEnumValueThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new InferredValue<PrimitiveDataType>(
                (PrimitiveDataType)999,
                Ratio.One,
                null,
                needsReview: true));
    }
}
