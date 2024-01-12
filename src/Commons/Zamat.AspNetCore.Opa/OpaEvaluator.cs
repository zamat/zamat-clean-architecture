using System.Threading.Tasks;
using AUMS.AspNetCore.Opa.Abstractions;

namespace AUMS.AspNetCore.Opa;

internal class OpaEvaluator : IOpaEvaluator
{
    private readonly OpaClient _opaClient;

    public OpaEvaluator(OpaClient opaClient)
    {
        _opaClient = opaClient;
    }

    public async Task<Output> EvaluateAsync(string opaPolicyPath, Input input)
    {
        return await _opaClient.EvaluateAsync(opaPolicyPath, input);
    }
}
