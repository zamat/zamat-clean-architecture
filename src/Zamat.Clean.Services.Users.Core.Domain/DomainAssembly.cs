using System.Reflection;

namespace Zamat.Clean.Services.Users.Core.Domain;

public static class DomainAssembly
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}
