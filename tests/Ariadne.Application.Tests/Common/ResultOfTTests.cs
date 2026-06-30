using Ariadne.Application.Common;

namespace Ariadne.Application.Tests.Common;

public class ResultOfTTests
{
    [Fact]
    public void SuccessCreatesSuccessfulResultWithValue()
    {
        var response = new SampleResponse("project-1");

        Result<SampleResponse> result = Result.Success(response);

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(response, result.Value);
        Assert.Null(result.Error);
    }

    [Fact]
    public void SuccessRequiresValue()
    {
        Assert.Throws<ArgumentNullException>(() => Result.Success<SampleResponse>(null!));
    }

    [Fact]
    public void FailureCreatesFailedResultWithError()
    {
        ApplicationError error = ApplicationError.NotFound(
            "PROJECT_NOT_FOUND",
            "Project was not found.");

        Result<SampleResponse> result = Result.Failure<SampleResponse>(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void FailureCanRepresentExpectedValidationErrorWithoutException()
    {
        Result<SampleResponse> result = Result.Failure<SampleResponse>(
            "PROJECT_NAME_REQUIRED",
            "Project name is required.");

        Assert.True(result.IsFailure);
        Assert.Equal(ApplicationErrorKind.Validation, result.Error?.Kind);
        Assert.Equal("PROJECT_NAME_REQUIRED", result.Error?.Code);
    }

    [Fact]
    public void FailureRequiresError()
    {
        Assert.Throws<ArgumentNullException>(() => Result.Failure<SampleResponse>(null!));
    }

    [Fact]
    public void AccessingValueOnFailureThrowsInvalidOperationException()
    {
        Result<SampleResponse> result = Result.Failure<SampleResponse>(
            "PROJECT_NAME_REQUIRED",
            "Project name is required.");

        Assert.Throws<InvalidOperationException>(() => result.Value);
    }

    private sealed record SampleResponse(string ProjectId);
}
