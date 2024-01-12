using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AUMS.AspNetCore.BackendForFrontend.Abstractions;
internal interface IRealmValidator
{
    Task<bool> TryValidateAsync(HttpContext context, string realm, string returnUrl);
}
