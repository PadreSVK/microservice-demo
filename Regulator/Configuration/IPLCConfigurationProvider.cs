using System;
using System.Threading.Tasks;

namespace Regulator.Configuration
{
    // ReSharper disable InconsistentNaming
    public interface IPLCConfigurationProvider
    {
        Task<PLCConfig> GetPLCConfig();
    }

    internal class PlcConfigurationProvider : IPLCConfigurationProvider
    {
        public async Task<PLCConfig> GetPLCConfig()
        {
            await Task.Delay(100);
            return new PLCConfig();
        }
    }


    public class ServiceA
    {
        private readonly Func<Task<PLCConfig>> config;

        public ServiceA(Func<Task<PLCConfig>> config)
        {
            this.config = config;
        }

        public async Task Test()
        {
            var plcConfig = await config();

            Console.WriteLine(plcConfig.Name);
            Console.WriteLine("test asdasd asd asd as");
        }
    }
}