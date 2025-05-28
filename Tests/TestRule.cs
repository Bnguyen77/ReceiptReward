using ReceiptReward.Interfaces;
using ReceiptReward.Tests;

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
			return (false, $" Unknown test type: {testCase.Type}");
		}

		return rule(testCase, service);
	}

	private static (bool, string) SingleInputTest(TestCase test, IReceiptProcessingService service)
	{
		if (test.LoadedReceipts.Count != 1 || test.Inputs.Count != 1)
			return (false, " singleInputTest requires exactly one input and one expectedOutput");

		var receipt = test.LoadedReceipts[0];
		var expected = test.Inputs[0].ExpectedOutput;

		var id = service.ProcessReceipt(receipt);
		var sss = service.GetPointsById(id);
		var actual = service.GetPointsById(id).ToString();
		var success = actual == expected;

		var summary = $"Expected: {expected}, Actual: {actual}, Result: {(success ? " PASS" : " FAIL")}";
		return (success, summary);
	}

}
