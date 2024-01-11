using Zamat.BuildingBlocks.Domain;

namespace Zamat.Clean.Services.Users.Core.Domain.ValueObjects;

public class FullName(string firstName, string lastName) : ValueObject<FullName>
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}
