using ReceiptPoints.Components;
using ReceiptPoints.Models;

namespace ReceiptPoints.Services
{
	public class ReceiptProcessingService : IReceiptProcessingService
	{
		private readonly InMemoryStorage _storageService;
		private readonly RewardCalculator _rewardCalculator;

		public ReceiptProcessingService(InMemoryStorage storageService, RewardCalculator calculator)
		{
			_storageService = storageService;
			_rewardCalculator = calculator;
		}

		public string ProcessReceipt(Receipt receipt)
		{
			var id = Guid.NewGuid().ToString();

			int points = 0;
			points += _rewardCalculator.RetailerAlphanumaricPoint(receipt.Retailer);
			points += _rewardCalculator.TotalRoundDollarPoint(receipt.Total);
			points += _rewardCalculator.TotalMultipleQuarterPoint(receipt.Total);
			points += _rewardCalculator.ItemPairsPoint(receipt.Items.Count);
			points += _rewardCalculator.ItemDescPoint(receipt.Items);

			if (DateOnly.TryParseExact(receipt.PurchaseDate, "yyyy-MM-dd", out var purchaseDate))
			{
				points += _rewardCalculator.OddPurchaseDatePoint(purchaseDate);
			}

			points += _rewardCalculator.AfternoonPurchaseTimePoint(receipt.PurchaseTime);

			_storageService.Save(id, points);
			return id;
		}

		public int GetPointsById(string id)
		{
			return _storageService.TryGet(id, out var points) ? points : -1;
		}
	}
}
