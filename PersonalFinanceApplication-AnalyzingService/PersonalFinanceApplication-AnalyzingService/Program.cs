using MassTransit;
using Microsoft.EntityFrameworkCore;
using PFA_DAL;
using PFA_DAL.Abstraction;
using PFA_DAL.Implementation;
using PFA_MBService.ServiceProperties;
using PFA_Services.Abstractions;
using PFA_Services.AIInsightService;
using PFA_Services.BalanceProcessingService;
using PFA_Services.ConsumerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddScoped<IBalanceProcessingService, BalanceProcessingService>();
builder.Services.AddScoped<IAccountBalanceRepository, AccountBalanceRepository>();
builder.Services.AddHttpClient<IAIInsightService, AIInsightService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:11434");
});

//rabbitMQSConfig
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
var rabbitSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();



builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ExpenseBalanceConsumer>();
    x.AddConsumer<IncomeBalanceConsumer>();
    x.AddConsumer<CreateUserContractConsumer>();
    x.AddConsumer<AiInsightConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri($"rabbitmq://{rabbitSettings.HostName}:{rabbitSettings.Port}/"), h =>
        {
            h.Username(rabbitSettings.UserName);
            h.Password(rabbitSettings.Password);
        });

        cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(false));
        cfg.UseRawJsonSerializer();
        cfg.ConfigureEndpoints(context);
    });
});


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

var isInDocker = Environment.GetEnvironmentVariable("DOCKER_LOCAL_CONTAINER_RUNNING") == "true";

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
