using ReceiptReward.Interfaces;
using ReceiptReward.Testing;

public static class TestRule
{
	private static readonly Dictionary<string, Func<TestCase, IReceiptProcessingService, (bool, string)>> _rules
		= new()
	{
		{ "singleinputtest", SingleInputTest }
	};

	public static (bool, string) Run(TestCase testCase, IReceiptProcessingService service)
	{
		var key = testCase.Type.Trim().ToLower();
		if (!_rules.TryGetValue(key, out var rule))
		{
			return (false, $"❌ Unknown test type: {testCase.Type}");
		}

		return rule(testCase, service);
	}

	private static (bool, string) SingleInputTest(TestCase test, IReceiptProcessingService service)
	{
		var id = service.ProcessReceipt(test.Input);
		var actual = service.GetPointsById(id).ToString();
		var success = actual == test.ExpectedOutput;

		var summary = $"Expected: {test.ExpectedOutput}, Actual: {actual}, Result: {(success ? "✅ PASS" : "❌ FAIL")}";
		return (success, summary);
	}

	// Future types: Add more methods and register them in _rules
}
