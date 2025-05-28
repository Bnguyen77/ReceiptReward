using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReceiptReward.Models
{
	public class Receipt
	{
		[Required]
		[RegularExpression(@"^[\w\s\-&]+$")]
		[JsonPropertyName("retailer")]
		public string Retailer { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		[JsonPropertyName("purchaseDate")]
		public string PurchaseDate { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Time)]
		[JsonPropertyName("purchaseTime")]
		public string PurchaseTime { get; set; } = string.Empty;

		[Required]
		[MinLength(1)]
		[JsonPropertyName("items")]
		public List<Item> Items { get; set; } = new();

		[Required]
		[RegularExpression(@"^\d+\.\d{2}$")]
		[JsonPropertyName("total")]
		public string Total { get; set; } = string.Empty;
	}
}
