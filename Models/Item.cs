using System.ComponentModel.DataAnnotations;

namespace ReceiptPoints.Models
{
	public class Item
	{
		[Required]
		[RegularExpression(@"^[\w\s\-]+$")]
		public string ShortDescription { get; set; } = string.Empty;

		[Required]
		[RegularExpression(@"^\d+\.\d{2}$")]
		public string Price { get; set; } = string.Empty;
	}
}
