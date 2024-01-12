using System.Threading;
using System.Threading.Tasks;

namespace AUMS.Common.EmailSender;

public interface IEmailSender
{
    bool Enabled { get; }
    Task SendEmailAsync(string email, EmailDto emailData, CancellationToken cancellationToken = default);
}
