using MassTransit.Testing;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Zamat.AspNetCore.Tests.Fixtures;

/// <summary>
/// A base class for integration tests that provides a web application factory, an HTTP client, and a MassTransit test harness.
/// This class is used when testing applications that use MassTransit for messaging.
/// </summary>
/// <typeparam name="TWebAppFactory">The type of the web application factory.</typeparam>
/// <typeparam name="TEntryPoint">The type of the entry point to the application.</typeparam>
public abstract class WebAppFixtureWithMassTransit<TWebAppFactory, TEntryPoint>
    : WebAppFixture<TWebAppFactory, TEntryPoint>
    where TWebAppFactory : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    /// <summary>
    /// The MassTransit test harness used for testing message bus interactions.
    /// </summary>
    protected readonly ITestHarness TestHarness;

    protected WebAppFixtureWithMassTransit(TWebAppFactory webFactory)
        : base(webFactory)
    {
        TestHarness = webFactory.Services.GetTestHarness();
    }
}
