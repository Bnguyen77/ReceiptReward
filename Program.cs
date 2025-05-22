using ReceiptReward.Startup;

var builder = WebApplication.CreateBuilder(args);
AppLauncher.Configure(builder);

var app = builder.Build();
AppLauncher.Run(app);
