using ReceiptReward.Models;

namespace ReceiptReward.Interfaces
{
	public interface IRewardCalculator
	{
		int RetailerAlphanumaricPoint(string retailer);
		int TotalRoundDollarPoint(string total);
		int TotalMultipleQuarterPoint(string total);
		int ItemPairsPoint(int itemCount);
		int ItemDescPoint(IEnumerable<Item> items);
		int AfternoonPurchaseTimePoint(string purchaseTime);
		int OddPurchaseDatePoint(DateOnly date);
	}
}