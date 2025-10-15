using Api.Enums;
using Api.Models;

namespace Api.Services;

public interface IEmailService
{
    Task<bool> SendMailAsync(SendEmailData mailData, EmailTypes emailType);
}
