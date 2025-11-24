using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using WeatherViewer.models;

namespace WeatherViewer.services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "76e764be39cd02f3147dd5329226edc5";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"{BaseUrl}/weather?q={city}&appid={ApiKey}&units=metric&lang=ru";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Ошибка API: {response.StatusCode}");
                    return null;
                }
                var json = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(json);

                var root = document.RootElement;

                return new WeatherData
                {
                    City = root.GetProperty("name").GetString(),
                    Country = root.GetProperty("sys").GetProperty("country").GetString(),
                    Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                    Description = root.GetProperty("weather")[0].GetProperty("description").GetString(),
                    Icon = root.GetProperty("weather")[0].GetProperty("icon").GetString(),
                    Humidity = root.GetProperty("main").GetProperty("humidity").GetDouble(),
                    WindSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble(),
                    LastUpdated = DateTime.Now
                };
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                return null;
            }
        }

        public List<City> GetPopularCities()
        {
            return new List<City>
        {
            new City { Name = "Moscow", Country = "RU" },
            new City { Name = "London", Country = "GB" },
            new City { Name = "New York", Country = "US" },
            new City { Name = "Paris", Country = "FR" },
            new City { Name = "Tokyo", Country = "JP" },
            new City { Name = "Berlin", Country = "DE" },
            new City { Name = "Sydney", Country = "AU" },
            new City { Name = "Dubai", Country = "AE" }
        };
        }
    }
}
