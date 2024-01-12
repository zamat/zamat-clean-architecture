using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using AUMS.AspNetCore.Authentication.Basic.Abstractions;
using Microsoft.Extensions.Options;

namespace AUMS.AspNetCore.Authentication.Basic.Store.Json;

internal class JsonBasicAuthenticator : IBasicAuthenticator
{
    private readonly IOptions<JsonBasicAuthenticatorOptions> _options;

    public JsonBasicAuthenticator(IOptions<JsonBasicAuthenticatorOptions> options)
    {
        _options = options;
    }

    public async Task<BasicAuthResult> AuthenticateAsync(BasicAuthenticationCredentials basicCredentials, CancellationToken cancellationToken = default!)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true
        };

        BasicAuthUsers usersFromJsonStream;
        using (FileStream jsonStream = new(_options.Value.FilePath, FileMode.Open, FileAccess.Read))
        {
            usersFromJsonStream = (await JsonSerializer.DeserializeAsync<BasicAuthUsers>(jsonStream, jsonSerializerOptions, cancellationToken))!;
        }

        var user = usersFromJsonStream.Users.FirstOrDefault(c => c.UserName == basicCredentials.Username && c.Password == basicCredentials.Password);

        if (user is null)
        {
            return new BasicAuthResult(false, new List<Claim>());
        }

        return new BasicAuthResult(true, user.Claims.Select(c => new Claim(c.ClaimName, c.ClaimValue)));
    }
}
