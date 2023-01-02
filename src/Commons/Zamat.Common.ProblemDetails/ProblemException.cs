using System;

namespace Zamat.Common.ProblemDetails;

public class ProblemException : Exception
{
    public Problem Problem { get; init; }

    public ProblemException(Problem problem) : base(problem.Detail)
    {
        Problem = problem;
    }
}
