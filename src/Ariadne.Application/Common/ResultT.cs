namespace Ariadne.Application.Common;

public sealed class Result<T>
{
    private readonly T? _value;

    internal Result(bool isSuccess, T? value, ApplicationError? error)
    {
        if (isSuccess && value is null)
            throw new ArgumentNullException(nameof(value));

        if (isSuccess && error is not null)
            throw new ArgumentException("A successful result cannot include an error.", nameof(error));

        if (!isSuccess && error is null)
            throw new ArgumentException("A failed result requires an error.", nameof(error));

        IsSuccess = isSuccess;
        _value = value;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access the value of a failed result.");

    public ApplicationError? Error { get; }
}
