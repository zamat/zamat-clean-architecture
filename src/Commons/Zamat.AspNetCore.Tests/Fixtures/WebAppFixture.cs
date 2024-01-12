using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Zamat.AspNetCore.Tests.Fixtures;

/// <summary>
/// Provides a base class for testing web applications. This class is a fixture that can be used with xUnit to create a test context for integration tests.
/// </summary>
/// <typeparam name="TWebAppFactory">The type of the WebApplicationFactory.</typeparam>
/// <typeparam name="TEntryPoint">The type of the entry point for the application under test.</typeparam>
public abstract class WebAppFixture<TWebAppFactory, TEntryPoint> : IClassFixture<TWebAppFactory>
    where TWebAppFactory : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    /// <summary>
    /// The web application factory used to create instances of the web application under test.
    /// </summary>
    protected readonly TWebAppFactory WebFactory;

    /// <summary>
    /// The HTTP client used to send HTTP requests to the web application under test.
    /// </summary>
    protected readonly HttpClient HttpClient;

    protected WebAppFixture(TWebAppFactory webFactory)
    {
        WebFactory = webFactory;
        HttpClient = webFactory.CreateDefaultClient();
    }

    /// <summary>
    /// Converts an object of type TQuery into a query string. Each property of the object is transformed into a key-value pair in the query string.
    /// </summary>
    /// <param name="value">The object to be converted into a query string.</param>
    /// <typeparam name="TQuery">The type of the object.</typeparam>
    /// <returns>A query string representing the object.</returns>
    protected static string ConvertToQuery<TQuery>(TQuery value)
    {
        var typeProperties = typeof(TQuery).GetProperties();

        var queryStringComponents = typeProperties
            .Select(p => new { Property = p, Value = p.GetValue(value) })
            .Where(x => x.Value is not null)
            .Select(
                x => $"{x.Property.Name}={HttpUtility.UrlEncode(ConvertValueToString(x.Value))}"
            );

        return "?" + string.Join("&", queryStringComponents);
    }

    /// <summary>
    /// Sends a GET request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <param name="path">The Uri the request is sent to.</param>
    /// <returns>A tuple containing the HTTP status code and the response body.</returns>
    protected async Task<(HttpStatusCode Code, string Body)> GetAsync(string path)
    {
        var response = await HttpClient.GetAsync(path);
        var responseBody = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, responseBody);
    }

    /// <summary>
    /// Sends a GET request to the specified Uri as an asynchronous operation and deserializes the JSON response body to an instance of a type specified by a generic type parameter.
    /// </summary>
    /// <typeparam name="TResponse">The type of the object to deserialize to and return.</typeparam>
    /// <param name="path">The Uri the request is sent to.</param>
    /// <returns>A tuple containing the HTTP status code and the deserialized object from the response body.</returns>
    protected async Task<(HttpStatusCode Code, TResponse? Body)> GetAsync<TResponse>(string path)
    {
        var response = await HttpClient.GetAsync(path);
        var responseBody = await response.Content.ReadAsStringAsync();

        return (
            response.StatusCode,
            JsonSerializer.Deserialize<TResponse>(
                responseBody,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    AllowTrailingCommas = true
                }
            )
        );
    }

    /// <summary>
    /// Sends a POST request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TRequest">Object to serialize as HTTP post body.</typeparam>
    /// <param name="path">The Uri the request is sent to.</param>
    /// <param name="value">The value to be POSTed.</param>
    /// <returns>A tuple containing the HTTP status code and the response body.</returns>
    protected async Task<(HttpStatusCode Code, string Body)> PostAsync<TRequest>(string path, TRequest value)
    {
        string serializedPostBody;

        if (value is string bodyString)
        {
            serializedPostBody = bodyString;
        }
        else
        {
            serializedPostBody = ConvertToString(value);
        }

        var response = await HttpClient.PostAsync(
            path,
            new StringContent(serializedPostBody, Encoding.UTF8, "application/json")
        );

        var responseBody = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, responseBody);
    }

    /// <summary>
    /// Sends the post request to the client at the specified uri, receives the response, and then serializes it to the specified type.
    /// </summary>
    /// <typeparam name="TResponse">The type on which the response from the client should be deserialized.</typeparam>
    /// <typeparam name="TRequest">Request body.</typeparam>
    /// <param name="path">Address to which the request should be sent.</param>
    /// <param name="value">Body value.</param>
    /// <returns>Deserialized response. <see langword="null"/> otherwise.</returns>
    protected async Task<(HttpStatusCode Code, TResponse? Body)> PostAsync<TResponse, TRequest>(string path, TRequest value)
    {
        (HttpStatusCode code, string body) = await PostAsync(path, value);

        if (code == HttpStatusCode.BadRequest)
        {
            return (code, default(TResponse));
        }

        if (typeof(TResponse) == typeof(string))
        {
            return (code, (TResponse?)(body as object));
        }

        return (code, JsonSerializer.Deserialize<TResponse>(
            body,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            }
        ));
    }

    /// <summary>
    /// Sends a PUT request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TRequest">Object to serialize as HTTP put body.</typeparam>
    /// <param name="path">The Uri the request is sent to.</param>
    /// <param name="value">The value to be PUT.</param>
    /// <returns>A tuple containing the HTTP status code and the response body.</returns>
    protected async Task<(HttpStatusCode Code, string Body)> PutAsync<TRequest>(
        string path,
        TRequest value
    )
    {
        string serializedPostBody;

        if (value is string bodyString)
        {
            serializedPostBody = bodyString;
        }
        else
        {
            serializedPostBody = ConvertToString(value);
        }

        var response = await HttpClient.PutAsync(
            path,
            new StringContent(serializedPostBody, Encoding.UTF8, "application/json")
        );
        var responseBody = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, responseBody);
    }

    /// <summary>
    /// Sends a PUT request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TRequest">Object to serialize as HTTP patch body.</typeparam>
    /// <param name="path">The Uri the request is sent to.</param>
    /// <param name="value">The value to be PUT.</param>
    /// <returns>A tuple containing the HTTP status code and the response body.</returns>
    protected async Task<(HttpStatusCode Code, string Body)> PatchAsync<TRequest>(
        string path,
        TRequest value
    )
    {
        string serializedPatchBody;

        if (value is string bodyString)
        {
            serializedPatchBody = bodyString;
        }
        else
        {
            serializedPatchBody = ConvertToString(value);
        }

        var response = await HttpClient.PatchAsync(
            path,
            new StringContent(serializedPatchBody, Encoding.UTF8, "application/json")
        );
        var responseBody = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, responseBody);
    }

    /// <summary>
    /// Sends a DELETE request to the specified Uri as an asynchronous operation.
    /// </summary>
    /// <typeparam name="TQuery">Object to serialize as HTTP query string.</typeparam>
    /// <param name="path">The Uri the request is sent to.</param>
    /// <param name="value">The value to be PUT.</param>
    /// <returns>A tuple containing the HTTP status code and the response body.</returns>
    protected async Task<(HttpStatusCode Code, string Body)> DeleteAsync<TQuery>(
        string path,
        TQuery value
    )
    {
        string serializedQuery;

        if (value is string queryString)
        {
            serializedQuery = queryString;
        }
        else
        {
            serializedQuery = ConvertToQuery(value);
        }

        if (!serializedQuery.StartsWith('?'))
        {
            serializedQuery = "?" + serializedQuery;
        }

        var response = await HttpClient.DeleteAsync(path + serializedQuery);
        var responseBody = await response.Content.ReadAsStringAsync();

        return (response.StatusCode, responseBody);
    }

    private static string ConvertToString<T>(T value)
    {
        return JsonSerializer.Serialize(
            value,
            new JsonSerializerOptions
            {
                // TODO: Validate whether safe
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                AllowTrailingCommas = true
            }
        );
    }

    private static string ConvertValueToString(object? value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("O");
        }

        return value?.ToString() ?? string.Empty;
    }
}
