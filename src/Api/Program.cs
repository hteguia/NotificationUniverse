using Api;
using Api.Services;
using Api.Settings;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IAppSetting<MailSettings>, AppSetting<MailSettings>>();

builder.Services.AddTransient<IEmailService, MailtripService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder =>
    {
        builder
            .AllowAnyOrigin() // ou .WithOrigins("https://localhost:5002")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseCors("Open");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
