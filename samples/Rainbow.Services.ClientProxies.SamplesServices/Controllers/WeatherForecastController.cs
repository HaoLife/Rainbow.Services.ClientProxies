using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rainbow.Services.ClientProxies.Abstractions;
using Rainbow.Services.ClientProxies.SamplesServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Rainbow.Services.ClientProxies.SamplesServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceProxyFactory serviceProxyFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , IServiceProxyFactory serviceProxyFactory)
        {
            _logger = logger;
            this.serviceProxyFactory = serviceProxyFactory;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("{id}")]
        public WeatherForecast Get(int id, string test)
        {
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(id),
                TemperatureC = id,
                Summary = Summaries[0]
            };
        }



        [HttpGet("proxy/{id}")]
        public async Task<WeatherForecast> GetProxy(int id)
        {
            //return await serviceProxyFactory.Create<IWeatherForecastService>().GetAsync(id, "test");


            //var httpClient = new HttpClient();
            //HttpRequestMessage requestMessage = new HttpRequestMessage();
            //requestMessage.RequestUri = new Uri("https://localhost:5001/WeatherForecast/123");
            //requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            //requestMessage.Method = new HttpMethod("GET");
            //requestMessage.Content = new System.Net.Http.FormUrlEncodedContent(
            //    new List<KeyValuePair<string, string>> {
            //        new KeyValuePair<string, string>("test", "123") }
            //    );

            //var responseMessage = await httpClient.SendAsync(requestMessage);


            //requestMessage.Content = new System.Net.Http.StringContent();

            return null;
        }
    }
}
