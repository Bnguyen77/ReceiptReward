using ReceiptReward.Components;
using ReceiptReward.Extensions;
using ReceiptReward.Models;

namespace ReceiptReward.Services
{
	public class ReceiptProcessingService(IReceiptStorage storageService, RewardOrchestrator orchestrator) : IReceiptProcessingService
	{
		private readonly IReceiptStorage _storageService = storageService;
		private readonly RewardOrchestrator _orchestrator = orchestrator;

		public string ProcessReceipt(Receipt receipt)
		{
			var id = Guid.NewGuid().ToString();
			int points = _orchestrator.CalculatePoints(receipt);
			_storageService.Save(id, points);
			return id;
		}

		public int GetPointsById(string id) =>
			_storageService.TryGet(id, out var points) ? points : -1;
	}
}
