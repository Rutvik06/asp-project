namespace template.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		public string BookName { get; set; }
		public decimal BookPrice { get; set; }
		public int Quantity { get; set; }
		public decimal Total => BookPrice * Quantity;
	}
}
