using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Contrants.ServiceB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Regulator.Configurations;

namespace Regulator.HttpClients
{
    public class ServiceBClient
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<ServiceBClient> logger;

        public ServiceBClient(HttpClient httpClient, IOptions<ServiceBConfiguration> options, ILogger<ServiceBClient> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            httpClient.BaseAddress = new Uri(options.Value.Endpoint);
        }

        public async Task<ServiceBOutputMessage> SendData(ServiceBInputMessage message)
        {
            try
            {
                HttpResponseMessage postAsJsonAsync = await httpClient.PostAsJsonAsync("Receiver", message);
                return await postAsJsonAsync.Content.ReadFromJsonAsync<ServiceBOutputMessage>();
            }
            catch(Exception e)
            {
                logger.LogError(e, "Booom");
                return new ServiceBOutputMessage();
            }
        }
    }
}