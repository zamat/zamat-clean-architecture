namespace Zamat.Common.Http.DelegatingHandlers;

public class ProblemException(Problem problem) : Exception(problem.Detail)
{
    public Problem Problem { get; init; } = problem;
}
