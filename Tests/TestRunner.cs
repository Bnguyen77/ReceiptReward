using ReceiptReward.Config;
using ReceiptReward.Interfaces;
using ReceiptReward.Models;
using ReceiptReward.Tests;
using System.Text.Json;

public class TestRunner : ITestRunner
{
	private readonly IReceiptProcessingService _service;
	private readonly ILogger<TestRunner> _logger;
	private readonly string _testFolderPath;

	public TestRunner(IReceiptProcessingService service,
	ILogger<TestRunner> logger,
	AppConfig config)
	{
		_service = service;
		_logger = logger;
		_testFolderPath = $"{config.Test.Path}/{config.Test.Version}";
	}

	public void RunAllTests()
	{
		if (!Directory.Exists(_testFolderPath))
		{
			_logger.LogError(" Test folder not found: {Folder}", _testFolderPath);
			return;
		}

		var jsonFiles = Directory.GetFiles(_testFolderPath, "*.json");
		if (jsonFiles.Length == 0)
		{
			_logger.LogWarning(" No test files found in: {Folder}", _testFolderPath);
			return;
		}

		int totalPassed = 0;
		int totalTests = 0;

		foreach (var file in jsonFiles)
		{
			var json = File.ReadAllText(file);
			var test = JsonSerializer.Deserialize<TestCase>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			if (test == null)
			{
				_logger.LogWarning(" Failed to parse test case: {File}", Path.GetFileName(file));
				continue;
			}

			foreach (var inputRef in test.Inputs)
			{
				var inputPath = Path.Combine("Tests", "Inputs", "v01", inputRef.File);
				if (!File.Exists(inputPath))
				{
					_logger.LogError(" Input file not found: {File}", inputRef.File);
					continue;
				}

				var inputJson = File.ReadAllText(inputPath);

				var receipt = JsonSerializer.Deserialize<Receipt>(inputJson);
				if (receipt == null)
				{
					_logger.LogError(" Failed to deserialize input: {File}", inputRef.File);
					continue;
				}

				test.LoadedReceipts.Add(receipt);
				Console.WriteLine(test);
			}

			totalTests++;
			var (success, summary) = TestRule.Run(test, _service);
			if (success) totalPassed++;

			_logger.LogInformation(" {File} | Type: {Type} | {Summary}", Path.GetFileName(file), test.Type, summary);
		}

		_logger.LogInformation(" Overall Test Summary: {Passed}/{Total} passed", totalPassed, totalTests);
	}

}
