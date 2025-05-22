using ReceiptReward.Interfaces;
using ReceiptReward.Testing;
using System.Text.Json;

public static class TestRunner
{
	public static void Run(IReceiptProcessingService service, string testFile)
	{
		var logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger("TestRunner");

		if (!File.Exists(testFile))
		{
			logger.LogError("❌ Test file not found: {File}", testFile);
			return;
		}

		var json = File.ReadAllText(testFile);
		var testCases = JsonSerializer.Deserialize<List<TestCase>>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		});

		int passed = 0;
		foreach (var test in testCases!)
		{
			var id = service.ProcessReceipt(test.Input);
			var actual = service.GetPointsById(id);
			var success = actual == test.ExpectedPoints;

			if (success) passed++;
			logger.LogInformation("Test ID: {Id} | Expected: {Expected} | Actual: {Actual} | Result: {Result}",
				id, test.ExpectedPoints, actual, success ? "✅ PASS" : "❌ FAIL");
		}

		logger.LogInformation("🧪 Test Summary: {Passed}/{Total} passed", passed, testCases.Count);
	}
}
