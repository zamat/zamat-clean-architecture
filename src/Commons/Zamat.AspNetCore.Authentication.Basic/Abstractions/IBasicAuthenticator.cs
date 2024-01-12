namespace AUMS.AspNetCore.Authentication.Basic.Abstractions;

public interface IBasicAuthenticator
{
    Task<BasicAuthResult> AuthenticateAsync(BasicAuthenticationCredentials basicCredentials, CancellationToken cancellationToken = default!);
}
