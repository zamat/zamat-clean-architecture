using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using AUMS.AspNetCore.Opa.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace AUMS.AspNetCore.Opa;

internal class OpalClient : IOpalEventPublisher
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpalClient> _logger;

    public OpalClient(HttpClient httpClient, IOptions<OpalOptions> opalOptions, ILogger<OpalClient> logger)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(opalOptions.Value.BaseAddress);
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.UserAgent, "OpalClient");

        _httpClient.Timeout = TimeSpan.FromMilliseconds(opalOptions.Value.Timeout);
        _logger = logger;
    }

    public async Task PublishAsync(DataSourceUpdateEvent dataSourceUpdateEvent, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync("/data/config", dataSourceUpdateEvent, cancellationToken);
        _logger.LogInformation("Status: {response}", response);
    }
}
