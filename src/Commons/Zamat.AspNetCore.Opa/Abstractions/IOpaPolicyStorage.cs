using System.Threading.Tasks;

namespace AUMS.AspNetCore.Opa.Abstractions;

public interface IOpaPolicyStorage
{
    Task<OpaPolicyCollection> GetAsync(string? subPath);
    Task SaveAsync(string path, string rawPolicy);
}
