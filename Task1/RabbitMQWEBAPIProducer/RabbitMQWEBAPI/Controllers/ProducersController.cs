using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQWEBAPI.Producer.RabbitMQ;

namespace RabbitMQWEBAPI.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        readonly IRabbitMQProducer _rabbitMQProducer;

        public ProducersController(IRabbitMQProducer rabbitMQProducer)
        {
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet("produce")]
        public async Task<IActionResult> Set(string message)
        {
            _rabbitMQProducer.SendProductMessage(message);
            return Ok();
        }

    }
}
