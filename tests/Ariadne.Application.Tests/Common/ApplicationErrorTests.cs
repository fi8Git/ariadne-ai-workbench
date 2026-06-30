using Ariadne.Application.Common;

namespace Ariadne.Application.Tests.Common;

public class ApplicationErrorTests
{
    [Fact]
    public void CreateTrimsCodeMessageAndDetails()
    {
        var error = new ApplicationError(
            "  PROJECT_NAME_REQUIRED  ",
            "  Project name is required.  ",
            ApplicationErrorKind.Validation,
            "  Provided name was blank.  ");

        Assert.Equal("PROJECT_NAME_REQUIRED", error.Code);
        Assert.Equal("Project name is required.", error.Message);
        Assert.Equal(ApplicationErrorKind.Validation, error.Kind);
        Assert.Equal("Provided name was blank.", error.Details);
    }

    [Fact]
    public void CreateNormalizesBlankDetailsToNull()
    {
        var error = new ApplicationError(
            "PROJECT_NAME_REQUIRED",
            "Project name is required.",
            details: " ");

        Assert.Null(error.Details);
    }

    [Theory]
    [InlineData(null, "Message")]
    [InlineData("", "Message")]
    [InlineData(" ", "Message")]
    [InlineData("CODE", null)]
    [InlineData("CODE", "")]
    [InlineData("CODE", " ")]
    public void CreateWithMissingRequiredTextThrowsArgumentException(string? code, string? message)
    {
        Assert.Throws<ArgumentException>(
            () => new ApplicationError(code!, message!));
    }

    [Fact]
    public void CreateWithUndefinedKindThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new ApplicationError("PROJECT_NAME_REQUIRED", "Project name is required.", (ApplicationErrorKind)999));
    }

    [Fact]
    public void FactoryMethodsSetExpectedKinds()
    {
        Assert.Equal(ApplicationErrorKind.Validation, ApplicationError.Validation("VALIDATION", "Validation failed.").Kind);
        Assert.Equal(ApplicationErrorKind.NotFound, ApplicationError.NotFound("NOT_FOUND", "Not found.").Kind);
        Assert.Equal(ApplicationErrorKind.Conflict, ApplicationError.Conflict("CONFLICT", "Conflict.").Kind);
        Assert.Equal(ApplicationErrorKind.Unavailable, ApplicationError.Unavailable("UNAVAILABLE", "Unavailable.").Kind);
        Assert.Equal(ApplicationErrorKind.Unexpected, ApplicationError.Unexpected("UNEXPECTED", "Unexpected.").Kind);
    }
}
