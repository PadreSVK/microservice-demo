using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Contrants.ServiceA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PLC.ServiceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControlController : ControllerBase
    {
       
        private readonly ILogger<ControlController> _logger;

        public ControlController(ILogger<ControlController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{count?}", Name = "GetWeatherForecast")]
        public IEnumerable<ServiceAOutputMessage> Get(int? count = 15)
        {
            var faker = new Faker<ServiceAOutputMessage>()
                    .RuleFor(i=> i.CorrelationId, f=> f.Random.Guid())
                    .RuleFor(i=> i.Payload, f => f.Lorem.Sentence(5))
                ;
            return faker.Generate(count.Value);
            
        }
    }
}