using Microsoft.AspNetCore.Mvc;
using ReceiptReward.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.SuppressModelStateInvalidFilter = true;
});

AppLauncher.ConfigureLogging(builder.Logging);
AppLauncher.Configure(builder);

var app = builder.Build();
AppLauncher.Start(app);
