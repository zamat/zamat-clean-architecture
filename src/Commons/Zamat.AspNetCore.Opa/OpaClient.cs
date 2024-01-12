using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace AUMS.AspNetCore.Opa;

internal class OpaClient
{
    private readonly HttpClient _httpClient;

    public OpaClient(HttpClient httpClient, IOptions<OpaOptions> opaOptions)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(opaOptions.Value.BaseAddress);
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/json");
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.UserAgent, "OpaClient");
    }

    public Task<OpaPolicyCollection> GetAsync(string? subPath)
    {
        return _httpClient.GetFromJsonAsync<OpaPolicyCollection>("/v1/policies/" + subPath)!;
    }

    public Task SaveAsync(string path, string rawPolicy)
    {
        return _httpClient.PutAsync("/v1/policies/" + path, new StringContent(rawPolicy));
    }

    public async Task<Output> EvaluateAsync(string opaPolicyPath, Input input)
    {
        var response = await _httpClient.PostAsJsonAsync("/v1/data/" + opaPolicyPath, new { Input = input });
        if (!response.IsSuccessStatusCode)
        {
            return Output.Fail;
        }

        using var responseStream = await response.Content.ReadAsStreamAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return await JsonSerializer.DeserializeAsync<Output>(responseStream, options) ?? Output.Fail;
    }

}
