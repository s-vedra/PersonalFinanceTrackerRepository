using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceApplication_API.ReffitSettings;
using PersonalFinanceApplication_DAL;
using PersonalFinanceApplication_DAL.Abstraction;
using PersonalFinanceApplication_DAL.Implementation;
using PersonalFinanceApplication_DTO.DtoModels;
using PersonalFinanceApplication_MBService.ProducerService;
using PersonalFinanceApplication_MBService.ServiceProperties;
using PersonalFinanceApplication_Services.CommandHandlers;
using PersonalFinanceApplication_Services.CommandHandlers.ExpenseCommandHandlers;
using PersonalFinanceApplication_Services.CommandHandlers.ExpenseCommands;
using PersonalFinanceApplication_Services.CommandHandlers.IncomeCommandHandlers;
using PersonalFinanceApplication_Services.QueryHandlers.ExpenseQueryHandlers;
using PersonalFinanceApplication_Services.QueryHandlers.IncomeAndBalanceQueryHandlers;
using Refit;

var builder = WebApplication.CreateBuilder(args);

//configure CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//repositories
builder.Services.AddScoped<IIncomeRepository, IncomeRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

//command handlers
builder.Services.AddScoped<IRequestHandler<CreateExpenseCommand, int>, CreateExpenseCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateIncomeCommand, int>, CreateIncomeCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteExpenseCommand>, DeleteExpenseCommandHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteIncomeCommand>, DeleteIncomeCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateExpenseCommand>, UpdateExpenseCommandHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateIncomeCommand>, UpdateIncomeCommandHandler>();

//query handlers
builder.Services.AddScoped<IRequestHandler<GetBalanceQuery, BalanceDto>, GetBalanceQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetExpenseQuery, ExpenseDto>, GetExpenseQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetIncomeQuery, IncomeDto>, GetIncomeQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetExpensesQuery, List<ExpenseDto>>, GetExpensesQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetIncomesQuery, List<IncomeDto>>, GetIncomesQueryHandler>();


//validator
builder.Services.AddScoped<IValidator<CreateExpenseCommand>, CreateExpenseValidator>();
builder.Services.AddScoped<IValidator<DeleteExpenseCommand>, DeleteExpenseValidator>();
builder.Services.AddScoped<IValidator<UpdateExpenseCommand>, UpdateExpenseValidator>();
builder.Services.AddScoped<IValidator<CreateIncomeCommand>, CreateIncomeValidator>();
builder.Services.AddScoped<IValidator<DeleteIncomeCommand>, DeleteIncomeValidator>();
builder.Services.AddScoped<IValidator<UpdateIncomeCommand>, UpdateIncomeValidator>();
builder.Services.AddScoped<IValidator<GetExpenseQuery>, GetExpenseValidator>();
builder.Services.AddScoped<IValidator<GetIncomeQuery>, GetIncomeValidator>();

//services
builder.Services.AddScoped<IProducerService, ProducerService>();

//rabbitMQSConfig
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));


//refit settings
builder.Services.AddScoped(x => RestService.For<IProxyApi>(builder.Configuration["ApiSettings:ApiProxyUrl"]));

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

app.UseCors("AllowAllOrigins");

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
