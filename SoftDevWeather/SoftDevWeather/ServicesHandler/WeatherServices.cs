using SoftDevWeather.Models;
using SoftDevWeather.WeatherRestClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoftDevWeather.ServicesHandler
{
    public class WeatherServices
    {
        OpenWeatherMap<WeatherMainModel> _openWeatherRest = new OpenWeatherMap<WeatherMainModel>();
        public async Task<WeatherMainModel> GetWeatherDetails(string city)
        {
            var getWeatherDetails = await _openWeatherRest.GetAllWeathers(city);
            return getWeatherDetails;
        }
    }
}
