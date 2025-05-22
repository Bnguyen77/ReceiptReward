using ReceiptReward.Components;
using ReceiptReward.Config;
using ReceiptReward.Extensions;
using ReceiptReward.Interfaces;
using ReceiptReward.Services;
using System.Diagnostics;

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
			builder.Services.AddSingleton<RewardOrchestrator>();

			builder.Services.AddControllers();

			if (_config.Environment == "production")
			{
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen();
			}
		}

		public static void Run(WebApplication app)
		{
			var config = app.Services.GetRequiredService<AppConfig>();
			var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("AppLauncher");

			if (config.Environment?.Trim().ToLower() == "development")
			{
				logger.LogInformation("🔧 Running in DEVELOPMENT mode");

				var service = app.Services.GetRequiredService<IReceiptProcessingService>();
				TestRunner.Run(service, $"Testing/TestCases/{config.Version.ToLower()}.json");

				logger.LogInformation("✅ Exiting after test run.");
				Environment.Exit(0);
			}

			logger.LogInformation("🚀 Running in PRODUCTION mode");

			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseAuthorization();
			app.MapControllers();

			// 🌐 Only launch browser if not running in Docker
			if (!IsRunningInDocker())
			{
				try
				{
					var swaggerUrl = "http://localhost:8080/swagger";
					logger.LogInformation("🌐 Launching browser to: {Url}", swaggerUrl);
					Process.Start(new ProcessStartInfo
					{
						FileName = swaggerUrl,
						UseShellExecute = true
					});
				}
				catch (Exception ex)
				{
					logger.LogWarning(ex, "❌ Failed to launch browser.");
				}
			}

			logger.LogInformation("🧭 Application is now starting web server...");
			app.Run();
		}

		// ✅ Add this method anywhere in AppLauncher class
		private static bool IsRunningInDocker()
		{
			return File.Exists("/.dockerenv");
		}
	}
}
