using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Regulator.Configuration;
using Regulator.Json.Appsettings;

namespace PLC.ServiceB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository<Car, Guid> carRepository;
        private readonly PositionOption positionOption;
        private readonly IRepository<Module, Guid> moduleRepository;
        private readonly ServiceA serviceA;
        private readonly IRepository<User, Guid> userRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            ServiceA serviceA,
            IRepository<Module, Guid> moduleRepository,
            IRepository<User, Guid> userRepository,
            IRepository<Car, Guid> carRepository,
            IOptions<PositionOption> options
        )
        {
            _logger = logger;
            this.serviceA = serviceA;
            this.moduleRepository = moduleRepository;
            this.userRepository = userRepository;
            this.carRepository = carRepository;
            this.positionOption = options.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            if (positionOption.Name == "asdsa")
            {
                Console.WriteLine("Huraaa");
            }

            await serviceA.Test();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
        //[ApiExplorerSettings(IgnoreApi = true)]

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id">3fa85f64-5717-4562-b3fc-2c963f66afa6</param>
        /// <returns>Module </returns>
        [HttpGet("module/{id}")]
        public Task<Module> GetModule(Guid id)
        {
            return moduleRepository.GetById(id);
        }

        [HttpGet("user/{id}")]
        public Task<User> GetUser(Guid id)
        {
            return userRepository.GetById(id);
        }

        [HttpGet("car/{id}")]
        public Task<Car> GetCar(Guid id)
        {
            return carRepository.GetById(id);
        }
    }
}