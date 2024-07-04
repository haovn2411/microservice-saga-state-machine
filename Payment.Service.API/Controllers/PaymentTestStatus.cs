using Contract;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Payment.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTestStatus : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentTestStatus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost("test-success")]
        public async Task<IActionResult> PaymentSuccess(string orderId)
        {
            await _publishEndpoint.Publish(new PaymentSuccess
            {
                OrderId = Guid.Parse(orderId)
            });
            return Ok();
        }
        [HttpPost("test-fail")]
        public async Task<IActionResult> PaymentFail(string orderId)
        {
            await _publishEndpoint.Publish(new PaymentFailed
            {
                OrderId = Guid.Parse(orderId),
            });
            return Ok();
        }
    }
}
