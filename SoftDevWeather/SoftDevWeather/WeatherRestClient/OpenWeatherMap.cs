using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SoftDevWeather.WeatherRestClient
{
    public class OpenWeatherMap<T>
    {
        private const string OpenWeatherApi =
            "https://api.openweathermap.org/data/2.5/weather?units=metric&q=";
        private const string Key = "79b751264916c9e41833f98a15ec94e7";
        HttpClient _httpClient = new HttpClient();

        public async Task<T> GetAllWeathers(string city)
        {
            var json = await _httpClient.GetStringAsync(OpenWeatherApi + city + "&APPID=" + Key);
            var getWeatherModels = JsonConvert.DeserializeObject<T>(json);
            return getWeatherModels;
        }
    }
}
