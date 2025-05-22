using ReceiptReward.Config;
using ReceiptReward.Interfaces;
using ReceiptReward.Testing;

namespace ReceiptReward.Extensions
{
	public class EnvironmentOrchestrator
	{
		private readonly AppConfig _config;
		private readonly IReceiptProcessingService _service;

		public EnvironmentOrchestrator(AppConfig config, IReceiptProcessingService service)
		{
			_config = config;
			_service = service;
		}

		public void Run()
		{
			if (_config.Environment == "development")
			{
				Console.WriteLine("🔧 Running in DEVELOPMENT mode 111...");
				TestRunner.Run(_service, $"Testing/TestCases/{_config.Version.ToLower()}.json");
				Environment.Exit(0);
			}

			Console.WriteLine("🚀 Starting API in PRODUCTION mode...");
			// no-op, main will start Swagger
		}
	}
}
