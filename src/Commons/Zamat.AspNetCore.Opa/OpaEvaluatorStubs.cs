using System.Threading.Tasks;
using AUMS.AspNetCore.Opa.Abstractions;

namespace AUMS.AspNetCore.Opa;

internal class OpaEvaluatorStubs : IOpaEvaluator
{
    public async Task<Output> EvaluateAsync(string opaPolicyPath, Input input)
    {
        return await Task.FromResult(Output.Success);
    }
}
