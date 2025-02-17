using MediatR;
using Microsoft.Extensions.Options;
using PFA_DTOModels.Commands;
using PFA_DTOModels.DTOModels;
using PFA_Services.Abstractions;
using PFA_Services.CommandHandlers;
using PFA_Services.HelperMethods;

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

//configureServices
builder.Services.AddScoped<IRequestHandler<LoginRequestCommand, LoginResponseModel>, LoginRequestCommandHandler>();
builder.Services.AddScoped<IFirebaseAuthService, FirebaseAuthService>();
builder.Services.AddScoped<IJwtAuthService, JwtAuthService>();

//mediatR configuration
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//firebase settings
builder.Services.Configure<FirebaseSettings>(builder.Configuration.GetSection("FirebaseSettings"));
//jwt settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var app = builder.Build();

//firebase configuration 
var firebaseSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<FirebaseSettings>>().Value;
FirebaseAuthService.InitializeFirebase(firebaseSettings);

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
