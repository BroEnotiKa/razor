using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace BadNews.Repositories.Weather
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private const string DefaultWeatherImageUrl = "/images/cloudy.png";

        private Random Random { get; } = new Random();
        private OpenWeatherClient OpenWeatherClient { get; }

        public WeatherForecastRepository(IOptions<OpenWeatherOptions> weatherOptions)
        {
            OpenWeatherClient = new OpenWeatherClient(weatherOptions?.Value.ApiKey);
        }

        public async Task<WeatherForecast> GetWeatherForecastAsync()
        {
            var openWeatherForecast = await OpenWeatherClient.GetWeatherFromApiAsync();
            if (openWeatherForecast != null)
                return WeatherForecast.CreateFrom(openWeatherForecast);

            return BuildRandomForecast();
        }

        private WeatherForecast BuildRandomForecast()
        {
            var temperature = Random.Next(-20, 20 + 1);
            return new WeatherForecast
            {
                TemperatureInCelsius = temperature,
                IconUrl = DefaultWeatherImageUrl
            };
        }
    }
}
