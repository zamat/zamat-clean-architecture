using Zamat.BuildingBlocks.Domain;

namespace Zamat.Clean.Services.Users.Core.Domain.ValueObjects;

public class FullName : ValueObject<FullName>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }
}
