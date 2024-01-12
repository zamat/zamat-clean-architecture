using System.Threading.Tasks;
using AUMS.AspNetCore.Opa.Abstractions;

namespace AUMS.AspNetCore.Opa;

internal class OpaPolicyStorage : IOpaPolicyStorage
{
    private readonly OpaClient _opaClient;

    public OpaPolicyStorage(OpaClient opaClient)
    {
        _opaClient = opaClient;
    }

    public Task<OpaPolicyCollection> GetAsync(string? subPath)
    {
        return _opaClient.GetAsync(subPath)!;
    }

    public Task SaveAsync(string path, string rawPolicy)
    {
        return _opaClient.SaveAsync(path, rawPolicy);
    }
}
