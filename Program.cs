using ReceiptReward.Components;
using ReceiptReward.Config;
using ReceiptReward.Extensions;
using ReceiptReward.Interfaces;
using ReceiptReward.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var appConfig = AppConfig.Load("config.yml");

// Register loaded instance
builder.Services.AddSingleton(appConfig); // <--- IMPORTANT

// Dependency Injection setup
builder.Services.AddSingleton<RewardCalculator>();
builder.Services.AddSingleton<RewardOrchestrator>();
builder.Services.AddSingleton<IRewardCalculator, RewardCalculator>();
builder.Services.AddSingleton<IReceiptStorage, InMemoryStorage>();
builder.Services.AddSingleton<IReceiptProcessingService, ReceiptProcessingService>();

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
