using ReceiptReward.Startup;

var builder = WebApplication.CreateBuilder(args);

AppLauncher.ConfigureLogging(builder.Logging);
AppLauncher.Configure(builder);

var app = builder.Build();
AppLauncher.Start(app);
