using System.ComponentModel.DataAnnotations;

namespace ReceiptPoints.Models
{
	public class Receipt
	{
		[Required]
		[RegularExpression(@"^[\w\s\-&]+$")]
		public string Retailer { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Date)]
		public string PurchaseDate { get; set; } = string.Empty;

		[Required]
		[DataType(DataType.Time)]
		public string PurchaseTime { get; set; } = string.Empty;

		[Required]
		[MinLength(1)]
		public List<Item> Items { get; set; } = new();

		[Required]
		[RegularExpression(@"^\d+\.\d{2}$")]
		public string Total { get; set; } = string.Empty;
	}
}
