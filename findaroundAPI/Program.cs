﻿var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

HostCertConfig.CertPath = Environment.ExpandEnvironmentVariables(@builder.Configuration["CertPath"]);
HostCertConfig.CertPass = Environment.ExpandEnvironmentVariables(@builder.Configuration["CertPassword"]);

//builder.Host.ConfigureWebHostDefaults(webBuilder =>
//{
//    webBuilder.ConfigureKestrel(opt =>
//    {
//        opt.ListenAnyIP(80);

//        opt.ListenAnyIP(443, listOpt =>
//        {
//            listOpt.UseHttps(HostCertConfig.CertPath, HostCertConfig.CertPass);
//        });
//    });
//});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
    options.ListenAnyIP(443, listOpt =>
    {
        listOpt.UseHttps(HostCertConfig.CertPath, HostCertConfig.CertPass);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class HostCertConfig
{
    public static string CertPath { get; set; }
    public static string CertPass { get; set; }
}