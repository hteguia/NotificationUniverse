namespace Api.Settings;

public record MailSettings
{
    public string SenderName { get; init; } = "Nouvelle carte cadeau";
    public string SenderEmail { get; init; }
    public string Token { get; init; }
    public string Url { get; init; }
    public string RegisterUuid { get; init; }
    public string ResetPasswordUuid { get; init; }
    public string From { get; set; }
    public string NewGiftUuid { get; init; }
}
