namespace Api.Models;

public record SendEmailData(
    string RecipientEmail,
    string Subject,
    string senderName,
    string Body,
    string Attachment);
