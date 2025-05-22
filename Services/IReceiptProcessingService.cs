using ReceiptReward.Models;

namespace ReceiptReward.Services
{
	public interface IReceiptProcessingService
	{
		string ProcessReceipt(Receipt receipt);
		int GetPointsById(string id);
	}
}