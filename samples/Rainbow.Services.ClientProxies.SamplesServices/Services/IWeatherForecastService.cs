using Rainbow.Services.ClientProxies.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rainbow.Services.ClientProxies.SamplesServices.Services
{
    [HttpRemoteService("samples", true, "{proxy}")]
    public interface IWeatherForecastService
    {
        List<WeatherForecast> Get();

        //{service}/{proxy}/{action}/{id}?test=111
        //get
        WeatherForecast Get(int id, string test);

        ValueTask<WeatherForecast> GetAsync(int id, int test);
    }
}
