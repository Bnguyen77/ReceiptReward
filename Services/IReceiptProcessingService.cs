using ReceiptPoints.Models;

namespace ReceiptPoints.Services
{
	public interface IReceiptProcessingService
	{
		int GetPointsById(string id);
		string ProcessReceipt(Receipt receipt);
	}
}