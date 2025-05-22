using ReceiptReward.Models;

namespace ReceiptReward.Testing
{
	public class TestCase
	{
		public Receipt Input { get; set; } = new();
		public int ExpectedPoints { get; set; }
	}
}