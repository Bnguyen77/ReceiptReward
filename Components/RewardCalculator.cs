using ReceiptPoints.Models;

namespace ReceiptPoints.Components
{
	public class RewardCalculator
	{
		public int RetailerAlphanumaricPoint(string retailer)
		{
			int res = retailer?.Count(char.IsLetterOrDigit) ?? 0;
			return res;
		}

		public int TotalRoundDollarPoint(string total)
		{
			if (decimal.TryParse(total, out var totalDecimal))
			{
				return totalDecimal % 1 == 0 ? 50 : 0;
			}
			return 0;
		}

		public int TotalMultipleQuarterPoint(string total)
		{
			if (decimal.TryParse(total, out var totalDecimal))
			{
				return totalDecimal % 0.25m == 0 ? 25 : 0;
			}
			return 0;
		}

		public int ItemPairsPoint(int itemCount)
		{
			int res = itemCount / 2 * 5;
			return res;
		}

		public int ItemDescPoint(IEnumerable<Item> items)
		{
			int res = 0;
			foreach (Item item in items)
			{
				string trimmedDesc = item.ShortDescription.Trim();
				if (trimmedDesc.Length % 3 == 0 &&
					decimal.TryParse(item.Price, out var price))
				{
					res += (int)Math.Ceiling(price * 0.2m);
				}
			}
			return res;
		}

		public int OddPurchaseDatePoint(DateOnly purchaseDate)
		{
			int res = purchaseDate.Day % 2 != 0 ? 6 : 0;
			return res;
		}

		public int AfternoonPurchaseTimePoint(string purchaseTime)
		{
			int res = 0;
			if (TimeSpan.TryParse(purchaseTime, out var time))
			{
				var start = new TimeSpan(14, 0, 0); // 2:00 PM
				var end = new TimeSpan(16, 0, 0);   // 4:00 PM
				if (time > start && time < end)
				{
					res = 10;
				}
			}
			return res;
		}
	}
}
