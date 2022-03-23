using System.Threading.Tasks;
using Contrants.Regulator;
using Contrants.ServiceA;
using Contrants.ServiceB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Regulator.HttpClients;

namespace Regulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiverController : ControllerBase
    {
        private readonly ILogger<ReceiverController> logger;
        private readonly ServiceBClient serviceBClient;

        public ReceiverController(ILogger<ReceiverController> logger, ServiceBClient serviceBClient)
        {
            this.logger = logger;
            this.serviceBClient = serviceBClient;
        }

        [HttpPost("plc-service-a")]
        public async Task<RegulatorOutputMessage> PLCServiceA([FromBody] ServiceAOutputMessage message)
        {
            logger.LogInformation("Receive message from serviceA {message}", message);


            ServiceBOutputMessage serviceBOutputMessage = await serviceBClient.SendData(new ServiceBInputMessage {
                CorrelationId = message.CorrelationId, Payload = "cupakabra"
            });

            return new RegulatorOutputMessage {
                CorrelationId = serviceBOutputMessage.CorrelationId,
                Payload = $"Ok - payload from serviceB {serviceBOutputMessage.Payload}"
            };
        }
    }
}