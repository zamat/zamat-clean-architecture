using System.Threading.Tasks;

namespace AUMS.AspNetCore.Opa.Abstractions;

internal interface IOpaEvaluator
{
    Task<Output> EvaluateAsync(string opaPolicyPath, Input input);
}
