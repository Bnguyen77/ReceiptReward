//using ReceiptReward.Components;
//using ReceiptReward.Extensions;
//using ReceiptReward.Interfaces;
//using ReceiptReward.Models;
//using ReceiptReward.Services;
//using ReceiptReward.Tests;
//using System.Text.Json;
//using Xunit;

//public class TestIntegration
//{
//	private readonly IReceiptProcessingService _service;

//	public TestIntegration()
//	{
//		// Build real dependencies
//		var storage = new InMemoryStorage();
//		var calculator = new RewardCalculator();
//		var orchestrator = new RewardOrchestrator(calculator);

//		_service = new ReceiptProcessingService(storage, orchestrator);
//	}

//	public static IEnumerable<object[]> AllTestCases()
//	{
//		var testDir = Path.Combine("Tests", "TestCases");
//		var inputDir = Path.Combine("Tests", "Inputs");

//		foreach (var file in Directory.GetFiles(testDir, "*.json"))
//		{
//			var json = File.ReadAllText(file);
//			var test = JsonSerializer.Deserialize<TestCase>(json, new JsonSerializerOptions
//			{
//				PropertyNameCaseInsensitive = true
//			});

//			if (test == null)
//				continue;

//			// Load inputs
//			foreach (var inputRef in test.Inputs)
//			{
//				var inputPath = Path.Combine(inputDir, inputRef.File);
//				if (!File.Exists(inputPath))
//					throw new FileNotFoundException($"Input file not found: {inputPath}");

//				var inputJson = File.ReadAllText(inputPath);
//				var receipt = JsonSerializer.Deserialize<Receipt>(inputJson);
//				if (receipt == null)
//					throw new InvalidOperationException($"Could not parse receipt from: {inputRef.File}");

//				test.LoadedReceipts.Add(receipt);
//			}

//			yield return new object[] { Path.GetFileName(file), test };
//		}
//	}

//	[Theory]
//	[MemberData(nameof(AllTestCases))]
//	public void Should_Pass_All_TestCases(string fileName, TestCase test)
//	{
//		var (success, summary) = TestRule.Run(test, _service);
//		Assert.True(success, $"❌ {fileName}: {summary}");
//	}
//}
