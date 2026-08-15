using Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Services.Notification;

public class EmailNotificationService : INotificationService
{
    private readonly EmailOptions _options;

    public EmailNotificationService(IOptions<EmailOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendOtpAsync(string destination, string code)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(
            _options.DisplayName,
            _options.From));

        email.To.Add(
            MailboxAddress.Parse(destination));

        email.Subject = "کد تایید ورود";

        email.Body = new TextPart("plain")
        {
            Text =
                $"""
                Hello,

                Your verification code is:

                {code}

                This code expires in 2 minutes.

                If you didn't request this code, please ignore this email.
                """
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _options.Host,
            _options.Port,
            SecureSocketOptions.StartTls);

        await client.AuthenticateAsync(
            _options.UserName,
            _options.Password);

        await client.SendAsync(email);

        await client.DisconnectAsync(true);
    }
}