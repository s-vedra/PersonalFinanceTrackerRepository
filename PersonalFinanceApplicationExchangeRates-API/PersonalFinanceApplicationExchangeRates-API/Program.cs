using PersonalFinanceApplicationExchangeRates_API.RefitSettings;
using PFA_Services.RequestService;
using Refit;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(x => RestService.For<IExchangeRatesClient>(builder.Configuration["ApiSettings:ExchangeRatesApi"]));
builder.Services.AddScoped(x => RestService.For<ICurrenciesClient>(builder.Configuration["ApiSettings:CurrenciesApi"]));
builder.Services.AddScoped<IRequestService, RequestService>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
