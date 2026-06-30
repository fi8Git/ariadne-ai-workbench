namespace Ariadne.Application.Common;

public sealed record ApplicationError
{
    public ApplicationError(
        string code,
        string message,
        ApplicationErrorKind kind = ApplicationErrorKind.Validation,
        string? details = null)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Application error code is required.", nameof(code));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Application error message is required.", nameof(message));

        if (!Enum.IsDefined(kind))
            throw new ArgumentOutOfRangeException(nameof(kind), kind, "Application error kind is not defined.");

        Code = code.Trim();
        Message = message.Trim();
        Kind = kind;
        Details = NormalizeOptionalText(details);
    }

    public string Code { get; }

    public string Message { get; }

    public ApplicationErrorKind Kind { get; }

    public string? Details { get; }

    public static ApplicationError Validation(string code, string message, string? details = null)
        => new(code, message, ApplicationErrorKind.Validation, details);

    public static ApplicationError NotFound(string code, string message, string? details = null)
        => new(code, message, ApplicationErrorKind.NotFound, details);

    public static ApplicationError Conflict(string code, string message, string? details = null)
        => new(code, message, ApplicationErrorKind.Conflict, details);

    public static ApplicationError Unavailable(string code, string message, string? details = null)
        => new(code, message, ApplicationErrorKind.Unavailable, details);

    public static ApplicationError Unexpected(string code, string message, string? details = null)
        => new(code, message, ApplicationErrorKind.Unexpected, details);

    private static string? NormalizeOptionalText(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
