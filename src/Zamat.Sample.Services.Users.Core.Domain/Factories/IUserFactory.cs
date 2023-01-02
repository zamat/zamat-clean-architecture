namespace Zamat.Sample.Services.Users.Core.Domain.Factories;

public interface IUserFactory
{
    User Create(string id, string userName, FullName fullName);
}
