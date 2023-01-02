using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Common.ProblemDetails;

public class ProblemDelegatingHandler : DelegatingHandler
{
    const string ContentType = "application/problem+json";

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
