using System.Threading;
using System.Threading.Tasks;

namespace AUMS.Common.SmsSender;

public interface ISmsSender
{
    bool Enabled { get; }
    Task SendSmsAsync(string phoneNumber, string text, CancellationToken cancellationToken = default);
}
