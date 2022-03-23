using System.Threading.Tasks;
using Contrants.ServiceB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PLC.ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiverController : ControllerBase
    {
        private readonly ILogger<ReceiverController> logger;

        public ReceiverController(ILogger<ReceiverController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ServiceBOutputMessage> Post(ServiceBInputMessage message)
        {
            logger.LogInformation($"Recieve message in PLC.ServiceB {message.CorrelationId}");
            return new ServiceBOutputMessage {
                CorrelationId = message.CorrelationId, Payload = "buuuuuuu from PLC.ServiceB"
            };
        }
    }
}