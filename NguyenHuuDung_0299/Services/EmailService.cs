using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace NguyenHuuDung_0299.Services;

public interface IEmailService
{
    Task SendOtpAsync(string recipient, string code, string purpose, CancellationToken cancellationToken);
}

public sealed class SmtpEmailService(IOptions<SmtpOptions> options, ILogger<SmtpEmailService> logger) : IEmailService
{
    private readonly SmtpOptions _options = options.Value;

    public async Task SendOtpAsync(string recipient, string code, string purpose, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.Host) || string.IsNullOrWhiteSpace(_options.FromEmail))
            throw new InvalidOperationException("SMTP chưa được cấu hình.");

        using var message = new MailMessage(_options.FromEmail, recipient)
        {
            Subject = $"Mã OTP - {purpose}",
            Body = $"Mã OTP của bạn là {code}. Mã có hiệu lực trong 5 phút.",
            IsBodyHtml = false
        };
        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            EnableSsl = _options.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Timeout = 10_000
        };
        if (!string.IsNullOrWhiteSpace(_options.Username))
            client.Credentials = new NetworkCredential(_options.Username, _options.Password);

        cancellationToken.ThrowIfCancellationRequested();
        await client.SendMailAsync(message, cancellationToken);
        logger.LogInformation("Sent {Purpose} OTP to {Email}", purpose, recipient);
    }
}
