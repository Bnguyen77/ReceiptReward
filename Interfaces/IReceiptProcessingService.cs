using ReceiptReward.Models;

namespace ReceiptReward.Interfaces
{
	public interface IReceiptProcessingService
	{
		string ProcessReceipt(Receipt receipt);
		int? GetPointsById(string id);
	}
}