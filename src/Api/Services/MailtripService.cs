using Api.Enums;
using Api.Models;
using Api.Settings;
using System.Text;
using System.Text.Json;

namespace Api.Services;

public class MailtripService(ILogger<MailtripService> logger, IAppSetting<MailSettings> mailSettings) : IEmailService
{
    public async Task<bool> SendMailAsync(SendEmailData mailData, EmailTypes emailType)
    {
        if (emailType == EmailTypes.GIFT_CARD_RECIPIENT)
        {
            return await MailGiftBeneficiary(mailData);
        }

        return await SendDefaultMail(mailData);
    }

    public async Task<bool> SendDefaultMail(SendEmailData mailData)
    {
        var emailModel = new
        {
            From = new
            {
                Email = mailSettings.Value.From,
                Name = mailData.senderName//mailSettings.Value.SenderName
            },
            To = new[] { new { Email = mailData.RecipientEmail } },
            mailData.Subject,
            Text = mailData.Body,
        };

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {mailSettings.Value.Token}");
        string requestContent = JsonSerializer.Serialize(emailModel);
        var stringContent = new StringContent(requestContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(mailSettings.Value.Url, stringContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var dataResponse = JsonSerializer.Deserialize<object>(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError(message: dataResponse!.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> MailGiftBeneficiary(SendEmailData mailData)
    {
        var emailModel = new
        {
            From = new
            {
                Email = mailSettings.Value.From,
                Name = mailSettings.Value.SenderName
            },
            To = new[] { new { Email = mailData.RecipientEmail } },
            Template_uuid = mailSettings.Value.NewGiftUuid,
            Template_variables = new
            {
                amount_value = mailData.Body
            },
            //mailData.Subject,
            attachments = new[]
            {
                new{
                content = mailData.Attachment,
                content_id = "qr-code",
                disposition = "attachment",
                filename = "cadeau.png"
                }
            }
        };

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {mailSettings.Value.Token}");
        string requestContent = JsonSerializer.Serialize(emailModel);
        var stringContent = new StringContent(requestContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(mailSettings.Value.Url, stringContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var dataResponse = JsonSerializer.Deserialize<object>(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError(message: dataResponse!.ToString());
            return false;
        }

        return true;
    }
}
