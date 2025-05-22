using Microsoft.AspNetCore.Mvc;
using ReceiptReward.Interfaces;
using ReceiptReward.Models;

namespace ReceiptReward.Controllers
{
	[ApiController]
	[Route("receipts")]
	public class RewardController : ControllerBase
	{
		private readonly IReceiptProcessingService _processingService;

		public RewardController(IReceiptProcessingService processingService)
		{
			_processingService = processingService;
		}

		[HttpPost("process")]
		public IActionResult ProcessReceipt([FromBody] Receipt receipt)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new { error = "Invalid receipt format. Please verify input." });
			}

			var id = _processingService.ProcessReceipt(receipt);
			return Ok(new { id });
		}

		[HttpGet("{id}/points")]
		public IActionResult GetPoints(string id)
		{
			int result = _processingService.GetPointsById(id);
			if (result != null)
			{
				return Ok(new { points = result });
			}

			return NotFound(new { error = "No receipt found for that ID." });
		}
	}
}
