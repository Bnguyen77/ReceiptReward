using ReceiptPoints.Components;
using ReceiptPoints.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection setup
builder.Services.AddSingleton<InMemoryStorage>();
builder.Services.AddSingleton<RewardCalculator>();
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
