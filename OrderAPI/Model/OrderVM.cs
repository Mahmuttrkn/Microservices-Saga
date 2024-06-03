namespace OrderAPI.Model
{
    public class OrderVM
    {
        public int BuyerId { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }
    }
}
