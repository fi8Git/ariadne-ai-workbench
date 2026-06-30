namespace Ariadne.Application.Common;

public sealed class Result
{
    private Result(bool isSuccess, ApplicationError? error)
    {
        if (isSuccess && error is not null)
            throw new ArgumentException("A successful result cannot include an error.", nameof(error));

        if (!isSuccess && error is null)
            throw new ArgumentException("A failed result requires an error.", nameof(error));

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public ApplicationError? Error { get; }

    public static Result Success()
        => new(isSuccess: true, error: null);

    public static Result Failure(ApplicationError error)
        => new(isSuccess: false, error ?? throw new ArgumentNullException(nameof(error)));

    public static Result Failure(
        string code,
        string message,
        ApplicationErrorKind kind = ApplicationErrorKind.Validation,
        string? details = null)
        => Failure(new ApplicationError(code, message, kind, details));

    public static Result<T> Success<T>(T value)
        => new(isSuccess: true, value, error: null);

    public static Result<T> Failure<T>(ApplicationError error)
        => new(isSuccess: false, value: default, error ?? throw new ArgumentNullException(nameof(error)));

    public static Result<T> Failure<T>(
        string code,
        string message,
        ApplicationErrorKind kind = ApplicationErrorKind.Validation,
        string? details = null)
        => Failure<T>(new ApplicationError(code, message, kind, details));
}
