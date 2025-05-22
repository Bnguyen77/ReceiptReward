using ReceiptReward.Config;
using ReceiptReward.Interfaces;
using ReceiptReward.Models;

namespace ReceiptReward.Extensions
{
	public class RewardOrchestrator
	{
		private readonly IRewardCalculator _calculator;
		private readonly AppConfig _config;

		public RewardOrchestrator(IRewardCalculator calculator, AppConfig config)
		{
			_calculator = calculator;
			_config = config;
		}

		public int CalculatePoints(Receipt receipt)
		{
			int points = 0;
			var steps = _config.Calculations.ContainsKey(_config.Version)
				? _config.Calculations[_config.Version]
				: new List<string>();

			foreach (var step in steps)
			{
				if (Enum.TryParse<CalculationStep>(step, out var calcStep))
				{
					points += ApplyCalculationStep(calcStep, receipt);
				}
			}

			return points;
		}

		private int ApplyCalculationStep(CalculationStep step, Receipt receipt)
		{
			return step switch
			{
				CalculationStep.RetailerAlphanumeric => _calculator.RetailerAlphanumaricPoint(receipt.Retailer),
				CalculationStep.TotalRoundDollar => _calculator.TotalRoundDollarPoint(receipt.Total),
				CalculationStep.TotalMultipleQuarter => _calculator.TotalMultipleQuarterPoint(receipt.Total),
				CalculationStep.ItemPairs => _calculator.ItemPairsPoint(receipt.Items.Count),
				CalculationStep.ItemDesc => _calculator.ItemDescPoint(receipt.Items),
				CalculationStep.OddPurchaseDate => DateOnly.TryParseExact(receipt.PurchaseDate, "yyyy-MM-dd", out var date)
					? _calculator.OddPurchaseDatePoint(date)
					: 0,
				CalculationStep.AfternoonPurchaseTime => _calculator.AfternoonPurchaseTimePoint(receipt.PurchaseTime),
				_ => 0
			};
		}
	}
}
