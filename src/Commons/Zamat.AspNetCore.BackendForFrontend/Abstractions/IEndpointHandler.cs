using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.BackendForFrontend.Abstractions;

public interface IEndpointHandler
{
    Task HandleAsync(HttpContext context);
}
