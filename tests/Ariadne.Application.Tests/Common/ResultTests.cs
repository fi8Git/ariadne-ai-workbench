using Ariadne.Application.Common;

namespace Ariadne.Application.Tests.Common;

public class ResultTests
{
    [Fact]
    public void SuccessCreatesSuccessfulResultWithoutError()
    {
        Result result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Fact]
    public void FailureCreatesFailedResultWithError()
    {
        ApplicationError error = ApplicationError.Validation(
            "PROJECT_NAME_REQUIRED",
            "Project name is required.");

        Result result = Result.Failure(error);

        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void FailureFactoryCanCreateValidationErrorWithoutException()
    {
        Result result = Result.Failure(
            "PROJECT_NAME_REQUIRED",
            "Project name is required.",
            details: "The provided project name was blank.");

        Assert.True(result.IsFailure);
        Assert.Equal(ApplicationErrorKind.Validation, result.Error?.Kind);
        Assert.Equal("PROJECT_NAME_REQUIRED", result.Error?.Code);
    }

    [Fact]
    public void FailureRequiresError()
    {
        Assert.Throws<ArgumentNullException>(() => Result.Failure(null!));
    }
}
