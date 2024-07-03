using Contract;
using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Database;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        //[HttpGet]
        //public IActionResult GetOrders()
        //{
        //    var listProduct = new ListOrder();
        //    var result = listProduct.getProducts();
        //    return Ok(result);
        //}
        //[HttpPost]
        //public IActionResult CreateOrder(OrderEntity productEntity)
        //{
        //    var result = new ListOrder();
        //    result.add(productEntity);
        //    return Ok(result.getProducts());
        //}
        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromQuery] string inventoryId, int quantity)
        {
            await _publishEndpoint.Publish(new OrderCreated
            {
               InventoryId = inventoryId,
               OrderId = Guid.NewGuid(),
               Quantity = quantity
            });
            return Ok();
        }

    }
}
