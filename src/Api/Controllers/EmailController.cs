using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailController(IEmailService emailService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SendEmailData sendEmailData)
    {
        await emailService.SendMailAsync(sendEmailData, Enums.EmailTypes.DEFAULT);

        return Ok();
    }

    //[HttpPost]
    //public async Task<IActionResult> Post()
    //{
    //    var img = "https://ik.imagekit.io/sql4gb61z/momo-round.png";
    //    using var httpClient = new HttpClient();
    //    byte[] imageBytes = await httpClient.GetByteArrayAsync(img);

    //    // Exemple : enregistrer en local
    //    await System.IO.File.WriteAllBytesAsync("image.png", imageBytes);

    //    var base64String = Convert.ToBase64String(imageBytes);
    //    var data = new SendEmailData(
    //        "kteguiaherve@gmail.com",
    //        $"Vous avez recu 10 FCFA en carte cadeau",
    //        $"10 FCFA",
    //        base64String);

    //    await emailService.SendMailAsync(data, Enums.EmailTypes.GIFT_CARD_RECIPIENT);

    //    return Ok();
    //}
}
