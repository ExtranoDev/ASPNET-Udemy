using Microsoft.AspNetCore.Mvc;
using WeatherAppProject.Models;

namespace WeatherAppProject.Controllers
{
    public class HomeController : Controller
    {
        readonly IEnumerable<CityWeatherModel> weatherData = [
            new CityWeatherModel() {
                CityUniqueCode = "LDN",
                CityName = "London",
                DateAndTime = DateTime.Parse("2030-01-01 8:00"),
                TemperatureFahrenheit = 33
            },
            new CityWeatherModel() {
                CityUniqueCode = "NYC",
                CityName = "London",
                DateAndTime = DateTime.Parse("2030-01-01 3:00"),
                TemperatureFahrenheit = 60
            },
            new CityWeatherModel() {
                CityUniqueCode = "PAR",
                CityName = "Paris",
                DateAndTime = DateTime.Parse("2030-01-01 9:00"),
                TemperatureFahrenheit = 82
            }
        ];

        [Route("/")]
        public IActionResult Index()
        {
            return View(weatherData);
        }
        [Route("/weather/{cityCode}")]
        public IActionResult Weather(string cityCode)
        {
            CityWeatherModel cityWeatherData = 
                weatherData.First(x => x.CityUniqueCode == cityCode);
            return View(cityWeatherData);
        }
    }
}
