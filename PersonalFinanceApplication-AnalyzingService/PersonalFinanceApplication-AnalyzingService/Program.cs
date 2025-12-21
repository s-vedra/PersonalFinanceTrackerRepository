using Microsoft.EntityFrameworkCore;
using PFA_DAL;
using PFA_DAL.Abstraction;
using PFA_DAL.Implementation;
using PFA_MBService.ConsumerService;
using PFA_MBService.ServiceProperties;
using PFA_Services.Abstractions;
using PFA_Services.AnalyzingService;
using PFA_Services.BalanceProcessingService;
using PFA_Services.HelperMethods;

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
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

var isInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

if (isInDocker)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(80, listenOptions =>
        {
            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
        });
    });
}

builder.Services.AddGrpc();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DataContext>();
    dbContext.Database.Migrate();
}

app.MapGrpcService<AccountBalanceRetriever>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
