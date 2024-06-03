namespace OrderAPI.Model
{
    public class OrderItemVM
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
