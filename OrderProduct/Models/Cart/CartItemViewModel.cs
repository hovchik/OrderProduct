namespace OrderProduct.Models.Cart
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int UnitQuantity { get; set; }
    }
}