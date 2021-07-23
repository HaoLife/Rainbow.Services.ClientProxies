using Rainbow.Services.ClientProxies.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rainbow.Services.ClientProxies.SamplesServices.Services
{
    [RemoteProxy("test")]
    public interface IWeatherForecastService
    {
        List<WeatherForecast> Get();

        Task<WeatherForecast> GetAsync(int id, string test);
    }
}
