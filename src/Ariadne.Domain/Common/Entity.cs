namespace Ariadne.Domain.Common;

public abstract class Entity<TId>(TId id) : IEquatable<Entity<TId>> where TId : notnull
{
    public TId Id { get; } = id;

    public bool Equals(Entity<TId>? other)
        => other is not null && EqualityComparer<TId>.Default.Equals(Id, other.Id);

    public override bool Equals(object? obj)
        => obj is not null && obj.GetType() == GetType() && Equals((Entity<TId>)obj);

    public override int GetHashCode()
        => EqualityComparer<TId>.Default.GetHashCode(Id);
}
