using System.Net.Http.Json;

namespace Zamat.Common.Http.DelegatingHandlers;

public class ProblemDelegatingHandler : DelegatingHandler
{
    private const string ContentType = "application/problem+json";

    public ProblemDelegatingHandler()
    {
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        var mediaType = response.Content.Headers.ContentType?.MediaType;
        if (mediaType != null && mediaType.Equals(ContentType, StringComparison.InvariantCultureIgnoreCase))
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<Problem>(cancellationToken: cancellationToken) ?? new Problem();
            throw new ProblemException(problemDetails);
        }

        return response;
    }
}
