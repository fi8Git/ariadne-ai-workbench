namespace Ariadne.Domain.Common;

internal static class DomainGuard
{
    public static TId EnsureNotDefaultId<TId>(TId id, Guid value, string message)
    {
        if (value == Guid.Empty)
            throw new DomainException(message);

        return id;
    }

    public static DateTimeOffset EnsureUtc(DateTimeOffset timestamp, string parameterName)
    {
        if (timestamp.Offset != TimeSpan.Zero)
            throw new DomainException($"{parameterName} must be a UTC timestamp.");

        return timestamp;
    }

    public static DateTimeOffset? EnsureUtc(DateTimeOffset? timestamp, string parameterName)
        => timestamp is null ? null : EnsureUtc(timestamp.Value, parameterName);
}
