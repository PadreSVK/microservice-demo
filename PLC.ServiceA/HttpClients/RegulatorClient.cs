using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Contrants.Regulator;
using Contrants.ServiceA;
using Microsoft.Extensions.Options;
using PLC.ServiceA.Configurations;

namespace PLC.ServiceA.HttpClients
{
    public class RegulatorClient
    {
        private readonly HttpClient httpClient;
        private readonly RegulatorConfiguration options;

        public RegulatorClient(HttpClient httpClient, IOptions<RegulatorConfiguration> options)
        {
            this.httpClient = httpClient;
            this.options = options.Value;
            httpClient.BaseAddress = new Uri(options.Value.Endpoint);
        }

        public async Task<RegulatorOutputMessage> SendData(ServiceAOutputMessage message)
        {
            var postAsJsonAsync = await httpClient.PostAsJsonAsync("Receiver/plc-service-a", message);
            return await postAsJsonAsync.Content.ReadFromJsonAsync<RegulatorOutputMessage>();
        }
    }
}