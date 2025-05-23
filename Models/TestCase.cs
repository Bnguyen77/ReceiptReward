using ReceiptReward.Models;
using System.Text.Json.Serialization;

namespace ReceiptReward.Testing
{
	public class TestInputReference
	{
		[JsonPropertyName("file")]
		public string File { get; set; } = null!;

		[JsonPropertyName("expectedOutput")]
		public string ExpectedOutput { get; set; } = null!;
	}

	public class TestCase
	{
		[JsonPropertyName("type")]
		public string Type { get; set; } = null!;

		[JsonPropertyName("inputs")]
		public List<TestInputReference> Inputs { get; set; } = new();

		[JsonIgnore]
		public List<Receipt> LoadedReceipts { get; set; } = new();
	}
}