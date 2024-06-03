using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Model;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderVM model)
        {
            Order order = new()
            {
                BuyerId = model.BuyerId,
                OrderItems = model.OrderItems.Select(oi => new OrderItem
                {
                    Count = oi.Count,
                    Price = oi.Price,
                    ProductId = oi.ProductId
                }).ToList(),
                OrderStatus = OrderStatus.Suspend, //Durum bilgisi başlangıç olduğu için suspend veriliyor.
                TotalPrice = model.OrderItems.Sum(oi => oi.Count * oi.Price),
                CreatedDate = DateTime.Now
            };

            await _context.AddAsync<Order>(order);

            await _context.SaveChangesAsync();
            return Ok(true);
        }

    }
}
