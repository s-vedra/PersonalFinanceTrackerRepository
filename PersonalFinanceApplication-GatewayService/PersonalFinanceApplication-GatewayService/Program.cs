using Microsoft.Extensions.Options;
using PersonalFinanceApplication_GatewayService.FirebaseService;
using PersonalFinanceApplication_GatewayService.Models;

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

//firebase settings
builder.Services.Configure<FirebaseSettings>(builder.Configuration.GetSection("FirebaseSettings"));
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
