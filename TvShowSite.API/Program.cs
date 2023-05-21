using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Text;
using System.Web.Http;
using TvShowSite.Core.Helpers;
using TvShowSite.Domain.System;
using System.Text.Json;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Container container = new Container();
container.Options.DefaultLifestyle = new AsyncScopedLifestyle();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder
             .WithOrigins("http://localhost:4200")
             .AllowAnyHeader()
             .AllowCredentials()
             .AllowAnyMethod();
    });
});

builder.Services.AddSingleton(provider =>
{
    RSA rsa = RSA.Create();

    rsa.ImportFromPem(JwtAuthenticationHelper.PublicKey);

    return new RsaSecurityKey(rsa);
});

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        SecurityKey rsa = builder.Services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

        options.IncludeErrorDetails = true;

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "TvShowSiteApiBackend",
            ValidAudience = "TvShowSitePortalFrontEnd",
            IssuerSigningKey = rsa
        };
    })
    .AddJwtBearer("Timeless", options =>
    {
        SecurityKey rsa = builder.Services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();

        options.IncludeErrorDetails = true;

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "TvShowSiteApiBackend",
            ValidAudience = "TvShowSitePortalFrontEnd",
            IssuerSigningKey = rsa
        };
    });

builder.Services.AddSimpleInjector(container, options =>
{
    options
        .AddAspNetCore()
        .AddControllerActivation();
});

builder.Services.AddHttpContextAccessor();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

new TvShowSite.Core.CoreInjector().RegisterServices(container);
new TvShowSite.Domain.DomainInjector().RegisterServices(container);
new TvShowSite.Data.DataInjector().RegisterServices(container);
new TvShowSite.Service.ServiceInjector().RegisterServices(container);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.UseSimpleInjector(container);

//app.UseHttpsRedirection();

app.UsePathBase("/api");

app.UseAuthentication();
app.UseRouting();
app.UseCors("AllowLocalhost");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

container.Verify();

app.Run();
