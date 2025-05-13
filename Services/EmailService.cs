using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendOrderConfirmationEmailAsync(string toEmail, string userName, string claimCode, string orderSummary)
    {
        // Read SMTP settings from appsettings.json
        var smtpHost = _configuration["SmtpSettings:Host"];
        var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
        var smtpUsername = _configuration["SmtpSettings:Username"];
        var smtpPassword = _configuration["SmtpSettings:Password"];
        var fromEmail = _configuration["SmtpSettings:FromEmail"];
        var fromName = _configuration["SmtpSettings:FromName"];

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(fromName, fromEmail));
        message.To.Add(new MailboxAddress(userName, toEmail));
        message.Subject = "Order Confirmation - BookNest";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <h1>Thank you for your order, {userName}!</h1>
                <p>Your order has been successfully placed.</p>
                <h2>Order Summary:</h2>
                <p>{orderSummary}</p>
                <h3>Your Claim Code: {claimCode}</h3>
                <p>Use this code to track or redeem your order.</p>
                <p>Thank you for shopping with us!</p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpHost, smtpPort, false); 
                await client.AuthenticateAsync(smtpUsername, smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw new Exception("Error sending email", ex);
        }
    }
}
