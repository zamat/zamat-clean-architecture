## Usage

```csharp
using System.Net.Http;
using Zamat.Sample.Services.Users.Api.Rest.Client.V1;

namespace Zamat.Sample.Services.Users.Api.Rest.Client;

internal class Usage
{
    public Usage()
    {
        var httpClient = new HttpClient();
        _ = new UsersClient(httpClient);
    }
}

```