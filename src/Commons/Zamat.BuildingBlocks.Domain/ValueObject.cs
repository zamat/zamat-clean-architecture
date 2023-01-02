namespace Zamat.BuildingBlocks.Domain;

using System.Collections.Generic;
using System.Linq;

public abstract class ValueObject<T> where T : ValueObject<T>
{
    public override bool Equals(object? obj)
    {
        if (obj is not T valueObject)
            return false;

        return EqualsCore(valueObject);
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();

    private bool EqualsCore(T other)
        => GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    public override int GetHashCode()
        => GetEqualityComponents()
            .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));

    public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        => !(a == b);

}
