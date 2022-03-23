using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Contrants.Regulator;
using Contrants.ServiceA;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PLC.ServiceA.HttpClients;

namespace PLC.ServiceA
{
    public class PLCReader : BackgroundService
    {
        private readonly ILogger<PLCReader> logger;
        private readonly RegulatorClient regulatorClient;

        public PLCReader(ILogger<PLCReader> logger, RegulatorClient regulatorClient)
        {
            this.logger = logger;
            this.regulatorClient = regulatorClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(5000);
            logger.LogInformation("start reading PLC on XYZ");
            while(!stoppingToken.IsCancellationRequested)
            {
                Faker<ServiceAOutputMessage>? faker = new Faker<ServiceAOutputMessage>()
                        .RuleFor(i => i.CorrelationId, f => f.Random.Guid())
                        .RuleFor(i => i.Payload, f => f.Lorem.Sentence(5))
                    ;
                ServiceAOutputMessage? message = faker.Generate();
                logger.LogInformation("PLCReader => {message}", message);
                RegulatorOutputMessage regulatorOutputMessage = await regulatorClient.SendData(message);

                logger.LogInformation(
                    $"id:{regulatorOutputMessage.CorrelationId}, payload => {regulatorOutputMessage.Payload}");

                await Task.Delay(500, stoppingToken);
            }

            logger.LogInformation("finish reading PLC on XYZ");
        }
    }
}