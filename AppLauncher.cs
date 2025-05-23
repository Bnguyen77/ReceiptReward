using ReceiptReward.Components;
using ReceiptReward.Config;
using ReceiptReward.Extensions;
using ReceiptReward.Interfaces;
using ReceiptReward.Services;

namespace ReceiptReward.Startup
{
	public static class AppLauncher
	{
		private static AppConfig _config = null!;

		public static void Configure(WebApplicationBuilder builder)
		{


			_config = AppConfig.Load("config.yml");
			builder.Services.AddSingleton(_config);

			builder.Services.AddSingleton<RewardCalculator>();
			builder.Services.AddSingleton<IRewardCalculator, RewardCalculator>();
			builder.Services.AddSingleton<IReceiptStorage, InMemoryStorage>();
			builder.Services.AddSingleton<IReceiptProcessingService, ReceiptProcessingService>();
			builder.Services.AddSingleton<ITestRunner, TestRunner>();
			builder.Services.AddSingleton<RewardOrchestrator>();

			builder.Services.AddControllers();

			if (_config.Environment == "production")
			{
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen();
			}
		}

		public static void ConfigureLogging(ILoggingBuilder logging)
		{
			logging.ClearProviders();
			logging.AddSimpleConsole(options =>
			{
				options.SingleLine = true;
				options.TimestampFormat = "";
				options.IncludeScopes = false;
				options.UseUtcTimestamp = false;
				options.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;
			});
			logging.SetMinimumLevel(LogLevel.Warning);
		}

		public static void Start(WebApplication app)
		{
			var config = app.Services.GetRequiredService<AppConfig>();
			var logger = app.Services.GetRequiredService<ILoggerFactory>()
							.CreateLogger("MyApp");

			logger.LogInformation("Running in {} mode", config.Environment?.Trim());

			if (config.Environment?.Trim().ToLower() == "development")
			{
				var testRunner = app.Services.GetRequiredService<ITestRunner>();
				testRunner.RunAllTests();
				logger.LogInformation("Exiting after test run.");
				Environment.Exit(0);
			}

			logger.LogInformation("🚀 Running in PRODUCTION mode");

			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}
