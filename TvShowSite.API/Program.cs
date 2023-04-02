using SimpleInjector;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using TvShowSite.Domain.System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Container container = new Container();
container.Options.DefaultLifestyle = new AsyncScopedLifestyle();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSimpleInjector(container, options =>
{
    options
        .AddAspNetCore()
        .AddControllerActivation();
});

builder.Services.AddHttpContextAccessor();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

container.Verify();

app.Run();
