using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReceiptReward.Models
{
	public class Item
	{
		[Required]
		[RegularExpression(@"^[\w\s\-]+$")]
		[JsonPropertyName("shortDescription")]
		public string ShortDescription { get; set; } = string.Empty;

		[Required]
		[RegularExpression(@"^\d+\.\d{2}$")]
		[JsonPropertyName("price")]
		public string Price { get; set; } = string.Empty;
	}
}
