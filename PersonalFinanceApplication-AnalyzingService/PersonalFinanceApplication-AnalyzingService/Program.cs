using Microsoft.EntityFrameworkCore;
using PFA_DAL;
using PFA_DAL.Abstraction;
using PFA_DAL.Implementation;
using PFA_MBService.ConsumerService;
using PFA_MBService.ServiceProperties;
using PFA_Services.Abstractions;
using PFA_Services.AnalyzingService;
using PFA_Services.BalanceProcessingService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddScoped<IConsumerService, ConsumerService>();
builder.Services.AddScoped<IBalanceProcessingService, BalanceProcessingService>();
builder.Services.AddHostedService<ListenerService>();
builder.Services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();

//rabbitMQSConfig
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
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
